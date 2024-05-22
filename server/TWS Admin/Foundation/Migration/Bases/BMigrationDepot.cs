using System.Linq.Expressions;
using System.Reflection;

using Foundation.Migration.Enumerators;
using Foundation.Migration.Interfaces.Depot;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;

using Microsoft.EntityFrameworkCore;

namespace Foundation.Migrations.Bases;
/// <summary>
///     Defines base behaviors for a <see cref="IMigrationDepot{TMigrationSet}"/>
///     implementation describing <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     shared behaviors.
///     
///     A <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/> provides methods to 
///     serve datasource safe transactions for <see cref="TMigrationSet"/>.
/// </summary>
/// <typeparam name="TMigrationSource">
///     What source implementation belongs this depot.
/// </typeparam>
/// <typeparam name="TMigrationSet">
///     Migration mirror concept that this depot handles.
/// </typeparam>
public abstract class BMigrationDepot<TMigrationSource, TMigrationSet>
    : IMigrationDepot<TMigrationSet>,
      IMigrationDepot_Read<TMigrationSet>,
      ImigrationDepot_Delete<TMigrationSet>
    where TMigrationSource : BMigrationSource<TMigrationSource>
    where TMigrationSet : class, IMigrationSet {

    readonly protected Action<DbContext, IMigrationSet[]>? Dispose;
    /// <summary>
    ///     Source to handle direct transactions (not-safe)
    /// </summary>
    readonly protected TMigrationSource Source;
    /// <summary>
    ///     DBSet handler into <see cref="Source"/> to handle fastlike transactions related to the <see cref="TMigrationSet"/> 
    /// </summary>
    readonly protected DbSet<TMigrationSet> Set;
    /// <summary>
    ///     Generates a new instance of a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/> base.
    /// </summary>
    /// <param name="source">
    ///     The <typeparamref name="TMigrationSource"/> that stores and handles the transactions for this <see cref="TMigrationSet"/> concept.
    /// </param>
    public BMigrationDepot(TMigrationSource source, Action<DbContext, IMigrationSet[]>? Dispose) {
        this.Source = source;
        this.Dispose = Dispose;
        Set = Source.Set<TMigrationSet>();
    }

    #region View Interface

    public Task<MigrationView<TMigrationSet>> View(MigrationViewOptions Options) {
        int range = Options.Range;
        int page = Options.Page;
        int amount = Set.Count();

        (int pages, int left) = Math.DivRem(amount, range);
        if (left > 0) {
            pages++;
        }

        int start = (page - 1) * range;
        int records = page == pages ? left : range;
        IQueryable<TMigrationSet> query = Set
            .Skip(start)
            .Take(records);

        int orderActions = Options.Orderings.Length;
        if (orderActions > 0) {
            Type setType = typeof(TMigrationSet);
            IOrderedQueryable<TMigrationSet> orderingQuery = default!;

            for (int i = 0; i < orderActions; i++) {
                ParameterExpression parameterExpression = Expression.Parameter(setType, $"X{i}");
                MigrationViewOrderOptions ordering = Options.Orderings[i];

                PropertyInfo property = setType.GetProperty(ordering.Property)
                    ?? throw new Exception($"Unexisted property ({ordering.Property}) on ({setType})");
                MemberExpression memberExpression = Expression.MakeMemberAccess(parameterExpression, property);
                UnaryExpression translationExpression = Expression.Convert(memberExpression, typeof(object));
                Expression<Func<TMigrationSet, object>> orderingExpression = Expression.Lambda<Func<TMigrationSet, object>>(translationExpression, parameterExpression);

                if (i == 0) {
                    orderingQuery = ordering.Behavior switch {
                        Enumerators.MIgrationViewOrderBehaviors.DownUp => query.OrderBy(orderingExpression),
                        Enumerators.MIgrationViewOrderBehaviors.UpDown => query.OrderByDescending(orderingExpression),
                        _ => query.OrderBy(orderingExpression),
                    };
                    continue;
                }

                orderingQuery = ordering.Behavior switch {
                    Enumerators.MIgrationViewOrderBehaviors.DownUp => orderingQuery.ThenBy(orderingExpression),
                    Enumerators.MIgrationViewOrderBehaviors.UpDown => orderingQuery.ThenByDescending(orderingExpression),
                    _ => orderingQuery.ThenBy(orderingExpression),
                };
            }
            query = orderingQuery;
        }

        TMigrationSet[] sets = [.. query];

        return Task.FromResult(new MigrationView<TMigrationSet>() {
            Amount = amount,
            Pages = pages,
            Page = page,
            Sets = sets,
        });
    }

    #endregion

    #region Create Interface

    /// <summary>
    ///     Creates a new record into the datasource.
    /// </summary>
    /// <param name="Set">
    ///     <see cref="TMigrationSet"/> to store.
    /// </param>
    /// <returns> 
    ///     The stored object. (Object Id is always auto-generated)
    /// </returns>
    public async Task<TMigrationSet> Create(TMigrationSet Set) {
        Set.EvaluateWrite();

        await this.Set.AddAsync(Set);
        await Source.SaveChangesAsync();
        Source.ChangeTracker.Clear();

        Dispose?.Invoke(Source, [Set]);
        return Set;
    }
    /// <summary>
    ///     Creates a collection of records into the datasource. 
    ///     <br>
    ///         Depending on <paramref name="Sync"/> the transaction performs different,
    ///         the operation iterates the desire collection to store and collects all the 
    ///         failures gathered during the operation.
    ///     </br>
    /// </summary>
    /// <param name="Sets">
    ///     The collection to store.
    /// </param>
    /// <param name="Sync">
    ///     Determines if the transaction should be broke at the first failure catched. This means that
    ///     the previous successfully stored objects will be kept as stored but the next ones objects desired
    ///     to be stored won't continue, the operation will throw new exception.
    /// </param>
    /// <returns>
    ///     A <see cref="MigrationTransactionResult{TSet}"/> that stores a collection of failures, and successes caught.
    /// </returns>
    public async Task<MigrationTransactionResult<TMigrationSet>> Create(TMigrationSet[] Sets, bool Sync = false) {
        TMigrationSet[] safe = [];
        MigrationTransactionFailure[] fails = [];

        foreach (TMigrationSet set in Sets) {
            try {
                set.EvaluateWrite();
                safe = [.. safe, set];
            } catch (Exception excep) {
                if (Sync) throw;
                MigrationTransactionFailure fail = new(set, excep);
                fails = [.. fails, fail];
            }
        }


        Source.ChangeTracker.Clear();
        await this.Set.AddRangeAsync(safe);
        await Source.SaveChangesAsync();

        Dispose?.Invoke(Source, Sets);
        return new(safe, fails);
    }

    #endregion

    #region Read interface
    public async Task<MigrationTransactionResult<TMigrationSet>> Read(Expression<Func<TMigrationSet, bool>> Predicate, MigrationReadBehavior Behavior) {
        IQueryable<TMigrationSet> query = Set.Where(Predicate);

        if (!query.Any())
            return new MigrationTransactionResult<TMigrationSet>([], []);
        TMigrationSet[] items = Behavior switch {
            MigrationReadBehavior.First => [await query.FirstAsync()],
            MigrationReadBehavior.Last => [await query.LastAsync()],
            MigrationReadBehavior.All => await query.ToArrayAsync(),
            _ => throw new NotImplementedException()
        };


        TMigrationSet[] successes = [];
        MigrationTransactionFailure[] failures = [];
        foreach (TMigrationSet item in items) {
            try {
                item.EvaluateRead();

                successes = [.. successes, item];
            } catch (Exception excep) {
                MigrationTransactionFailure failure = new(item, excep);
                failures = [.. failures, failure];
            }
        }

        return new(successes, failures);
    }
    #endregion

    #region Delete interface

    public Task<MigrationTransactionResult<TMigrationSet>> Delete(TMigrationSet[] Sets) {

        TMigrationSet[] safe = [];
        MigrationTransactionFailure[] fails = [];

        foreach (TMigrationSet set in Sets) {
            try {
                set.EvaluateWrite();
                safe = [.. safe, set];
            } catch (Exception excep) {
                MigrationTransactionFailure fail = new() {
                    Set = set,
                    System = excep,
                };
                fails = [.. fails, fail];
            }
        }

        this.Set.RemoveRange(safe);
        return Task.FromResult<MigrationTransactionResult<TMigrationSet>>(new(safe, []));
    }

    public async Task<TMigrationSet> Delete (TMigrationSet Set) {
        Set.EvaluateWrite();
        this.Set.Remove(Set);
        await Source.SaveChangesAsync();
        Source.ChangeTracker.Clear();
        return Set;
    }
    #endregion

}