using System.Linq.Expressions;
using System.Reflection;

using CSM_Foundation.Source.Enumerators;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Models;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;

namespace CSM_Foundation.Source.Bases;
/// <summary>
///     Defines base behaviors for a <see cref="ISourceDepot{TMigrationSet}"/>
///     implementation describing <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     shared behaviors.
///     
///     A <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/> provides methods to 
///     serve datasource safe transactions for <see cref="TSourceSet"/>.
/// </summary>
/// <typeparam name="TSource">
///     What source implementation belongs this depot.
/// </typeparam>
/// <typeparam name="TSourceSet">
///     Migration mirror concept that this depot handles.
/// </typeparam>
public abstract class BSourceDepot<TSource, TSourceSet>
    : ISourceDepot<TSourceSet>
    where TSource : BSource<TSource>
    where TSourceSet : class, ISourceSet {

    protected readonly IMigrationDisposer? Disposer;
    /// <summary>
    ///     Name to handle direct transactions (not-safe)
    /// </summary>
    protected readonly TSource Source;
    /// <summary>
    ///     DBSet handler into <see cref="Source"/> to handle fastlike transactions related to the <see cref="TSourceSet"/> 
    /// </summary>
    protected readonly DbSet<TSourceSet> Set;
    /// <summary>
    ///     Generates a new instance of a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/> base.
    /// </summary>
    /// <param name="source">
    ///     The <typeparamref name="TSource"/> that stores and handles the transactions for this <see cref="TSourceSet"/> concept.
    /// </param>
    public BSourceDepot(TSource source, IMigrationDisposer? Disposer) {
        Source = source;
        this.Disposer = Disposer;
        Set = Source.Set<TSourceSet>();
    }

    #region View 

    public Task<SetViewOut<TSourceSet>> View(SetViewOptions Options, Func<IQueryable<TSourceSet>, IQueryable<TSourceSet>>? include = null) {
        int range = Options.Range;
        int page = Options.Page;
        int amount = Set.Count();

        (int pages, int left) = Math.DivRem(amount, range);
        if (left > 0) {
            pages++;
        }

        int start = (page - 1) * range;
        int records = page == pages ? left : range;
        IQueryable<TSourceSet> query = Set
            .Skip(start)
            .Take(records);

        if (include != null) {
            query = include(query);
        }
        int orderActions = Options.Orderings.Length;
        if (orderActions > 0) {
            Type setType = typeof(TSourceSet);
            IOrderedQueryable<TSourceSet> orderingQuery = default!;

            for (int i = 0; i < orderActions; i++) {
                ParameterExpression parameterExpression = Expression.Parameter(setType, $"X{i}");
                SetViewOrderOptions ordering = Options.Orderings[i];

                PropertyInfo property = setType.GetProperty(ordering.Property)
                    ?? throw new Exception($"Unexisted property ({ordering.Property}) on ({setType})");
                MemberExpression memberExpression = Expression.MakeMemberAccess(parameterExpression, property);
                UnaryExpression translationExpression = Expression.Convert(memberExpression, typeof(object));
                Expression<Func<TSourceSet, object>> orderingExpression = Expression.Lambda<Func<TSourceSet, object>>(translationExpression, parameterExpression);
                if (i == 0) {
                    orderingQuery = ordering.Behavior switch {
                        MIgrationViewOrderBehaviors.DownUp => query.OrderBy(orderingExpression),
                        MIgrationViewOrderBehaviors.UpDown => query.OrderByDescending(orderingExpression),
                        _ => query.OrderBy(orderingExpression),
                    };
                    continue;
                }

                orderingQuery = ordering.Behavior switch {
                    MIgrationViewOrderBehaviors.DownUp => orderingQuery.ThenBy(orderingExpression),
                    MIgrationViewOrderBehaviors.UpDown => orderingQuery.ThenByDescending(orderingExpression),
                    _ => orderingQuery.ThenBy(orderingExpression),
                };
            }
            query = orderingQuery;
        }

        TSourceSet[] sets = [.. query];

        return Task.FromResult(new SetViewOut<TSourceSet>() {
            Amount = amount,
            Pages = pages,
            Page = page,
            Sets = sets,
        });
    }

    #endregion

    #region Create

    /// <summary>
    ///     Creates a new record into the datasource.
    /// </summary>
    /// <param name="Set">
    ///     <see cref="TSourceSet"/> to store.
    /// </param>
    /// <returns> 
    ///     The stored object. (Object Id is always auto-generated)
    /// </returns>
    public async Task<TSourceSet> Create(TSourceSet Set) {
        Set.EvaluateWrite();

        _ = await this.Set.AddAsync(Set);
        _ = await Source.SaveChangesAsync();
        Source.ChangeTracker.Clear();

        Disposer?.Push(Source, [Set]);
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
    ///     A <see cref="SourceTransactionOut{TSet}"/> that stores a collection of failures, and successes caught.
    /// </returns>
    public async Task<SourceTransactionOut<TSourceSet>> Create(TSourceSet[] Sets, bool Sync = false) {
        TSourceSet[] safe = [];
        SourceTransactionFailure[] fails = [];

        foreach (TSourceSet set in Sets) {
            try {
                set.EvaluateWrite();
                safe = [.. safe, set];
            } catch (Exception excep) {
                if (Sync) {
                    throw;
                }

                SourceTransactionFailure fail = new(set, excep);
                fails = [.. fails, fail];
            }
        }


        Source.ChangeTracker.Clear();
        await Set.AddRangeAsync(safe);
        _ = await Source.SaveChangesAsync();

        Disposer?.Push(Source, Sets);
        return new(safe, fails);
    }

    #endregion

    #region Read
    public async Task<SourceTransactionOut<TSourceSet>> Read(Expression<Func<TSourceSet, bool>> Predicate, MigrationReadBehavior Behavior, Func<IQueryable<TSourceSet>, IQueryable<TSourceSet>>? Include = null) {
        IQueryable<TSourceSet> query = Set.Where(Predicate);

        if (Include != null) {
            query = Include(query);
        }

        if (!query.Any()) {
            return new SourceTransactionOut<TSourceSet>([], []);
        }

        TSourceSet[] items = Behavior switch {
            MigrationReadBehavior.First => [await query.FirstAsync()],
            MigrationReadBehavior.Last => [await query.LastAsync()],
            MigrationReadBehavior.All => await query.ToArrayAsync(),
            _ => throw new NotImplementedException()
        };


        TSourceSet[] successes = [];
        SourceTransactionFailure[] failures = [];
        foreach (TSourceSet item in items) {
            try {
                item.EvaluateRead();

                successes = [.. successes, item];
            } catch (Exception excep) {
                SourceTransactionFailure failure = new(item, excep);
                failures = [.. failures, failure];
            }
        }

        return new(successes, failures);
    }
    #endregion

    #region Update 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Set"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<RecordUpdateOut<TSourceSet>> Update(TSourceSet Record) {
        TSourceSet? previous = await Set
            .Where(i => i.Id == Record.Id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        _ = Set.Update(Record);
        _ = await Source.SaveChangesAsync();

        Disposer?.Push(Source, Record);
        return new RecordUpdateOut<TSourceSet> {
            Previous = previous,
            Updated = Record,
        };
    }

    #endregion

    #region Delete

    public Task<SourceTransactionOut<TSourceSet>> Delete(TSourceSet[] Sets) {

        TSourceSet[] safe = [];
        SourceTransactionFailure[] fails = [];

        foreach (TSourceSet set in Sets) {
            try {
                set.EvaluateWrite();
                safe = [.. safe, set];
            } catch (Exception excep) {
                SourceTransactionFailure fail = new(set, excep);
                fails = [.. fails, fail];
            }
        }

        Set.RemoveRange(safe);
        return Task.FromResult<SourceTransactionOut<TSourceSet>>(new(safe, []));
    }

    public async Task<TSourceSet> Delete(TSourceSet Set) {
        Set.EvaluateWrite();
        _ = this.Set.Remove(Set);
        _ = await Source.SaveChangesAsync();
        Source.ChangeTracker.Clear();
        return Set;
    }

    public async Task<TSourceSet> Delete(int Id) {
        TSourceSet record = await Set
            .AsNoTracking()
            .Where(r => r.Id == Id)
            .FirstOrDefaultAsync()
            ?? throw new Exception("Trying to remove an unexist record");
        
        Set.Remove(record);
        await Source.SaveChangesAsync();

        return record;
    }

    #endregion
}