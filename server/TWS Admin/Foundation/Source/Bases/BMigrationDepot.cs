using System.Linq.Expressions;
using System.Reflection;

using CSM_Foundation.Core.Bases;
using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Enumerators;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Models;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;
using CSM_Foundation.Source.Quality.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Xunit;

namespace CSM_Foundation.Source.Bases;
//public abstract class BHistoryMigrationDepot<TMigrationSource, TMigrationSet> : BMigrationDepot<TMigrationSource, TMigrationSet> {
    
//}

/// <summary>
///     Defines base behaviors for a <see cref="IMigrationDepot{TMigrationSet}"/>
///     implementation describing <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     shared behaviors.
///     
///     A <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/> provides methods to 
///     serve datasource saved transactions for <see cref="TMigrationSet"/>.
/// </summary>
/// <typeparam name="TMigrationSource">
///     What source implementation belongs this depot.
/// </typeparam>
/// <typeparam name="TMigrationSet">
///     Migration mirror concept that this depot handles.
/// </typeparam>
public abstract class BMigrationDepot<TMigrationSource, TMigrationSet>
    : IMigrationDepot<TMigrationSet>
    where TMigrationSource : BSource<TMigrationSource>
    where TMigrationSet : class, ISourceSet {

    protected readonly IMigrationDisposer? Disposer;
    /// <summary>
    ///     Name to handle direct transactions (not-saved)
    /// </summary>
    protected readonly TMigrationSource Source;
    /// <summary>
    ///     DBSet handler into <see cref="Source"/> to handle fastlike transactions related to the <see cref="TMigrationSet"/> 
    /// </summary>
    protected readonly DbSet<TMigrationSet> Set;
    /// <summary>
    ///     Generates a new instance of a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/> base.
    /// </summary>
    /// <param name="source">
    ///     The <typeparamref name="TMigrationSource"/> that stores and handles the transactions for this <see cref="TMigrationSet"/> concept.
    /// </param>
    public BMigrationDepot(TMigrationSource source, IMigrationDisposer? Disposer) {
        Source = source;
        this.Disposer = Disposer;
        Set = Source.Set<TMigrationSet>();
    }

    #region View 

    public Task<SetViewOut<TMigrationSet>> View(SetViewOptions Options, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? include = null) {
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

        if (include != null) {
            query = include(query);
        }

        int orderActions = Options.Orderings.Length;
        if (orderActions > 0) {
            Type setType = typeof(TMigrationSet);
            IOrderedQueryable<TMigrationSet> orderingQuery = default!;

            for (int i = 0; i < orderActions; i++) {
                ParameterExpression parameterExpression = Expression.Parameter(setType, $"X{i}");
                SetViewOrderOptions ordering = Options.Orderings[i];

                PropertyInfo property = setType.GetProperty(ordering.Property)
                    ?? throw new Exception($"Unexisted property ({ordering.Property}) on ({setType})");
                MemberExpression memberExpression = Expression.MakeMemberAccess(parameterExpression, property);
                UnaryExpression translationExpression = Expression.Convert(memberExpression, typeof(object));
                Expression<Func<TMigrationSet, object>> orderingExpression = Expression.Lambda<Func<TMigrationSet, object>>(translationExpression, parameterExpression);
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

        TMigrationSet[] sets = [.. query];

        return Task.FromResult(new SetViewOut<TMigrationSet>() {
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
    ///     <see cref="TMigrationSet"/> to store.
    /// </param>
    /// <returns> 
    ///     The stored object. (Object Id is always auto-generated)
    /// </returns>
    public async Task<TMigrationSet> Create(TMigrationSet Set) {
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
    ///     the current successfully stored objects will be kept as stored but the next ones objects desired
    ///     to be stored won't continue, the operation will throw new exception.
    /// </param>
    /// <returns>
    ///     A <see cref="SourceTransactionOut{TSet}"/> that stores a collection of failures, and successes caught.
    /// </returns>
    public async Task<SourceTransactionOut<TMigrationSet>> Create(TMigrationSet[] Sets, bool Sync = false) {
        TMigrationSet[] saved = [];
        SourceTransactionFailure[] fails = [];

        foreach (TMigrationSet record in Sets) {
            try {
                AttachDate(record);
                record.EvaluateWrite();
                Source.ChangeTracker.Clear();
                this.Set.Attach(record);
                await Source.SaveChangesAsync();
                saved = [.. saved, record];
            } catch (Exception excep) {
                if (Sync) {
                    throw;
                }

                SourceTransactionFailure fail = new(record, excep);
                fails = [.. fails, fail];
            }
        }

        Disposer?.Push(Source, Sets);
        return new(saved, fails);
    }

    #endregion

    #region Read
    public async Task<SourceTransactionOut<TMigrationSet>> Read(Expression<Func<TMigrationSet, bool>> Predicate, MigrationReadBehavior Behavior, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? Include = null) {
        IQueryable<TMigrationSet> query = Set.Where(Predicate);

        if (Include != null) {
            query = Include(query);
        }

        if (!query.Any()) {
            return new SourceTransactionOut<TMigrationSet>([], []);
        }

        TMigrationSet[] items = Behavior switch {
            MigrationReadBehavior.First => [await query.FirstAsync()],
            MigrationReadBehavior.Last => [await query.LastAsync()],
            MigrationReadBehavior.All => await query.ToArrayAsync(),
            _ => throw new NotImplementedException()
        };


        TMigrationSet[] successes = [];
        SourceTransactionFailure[] failures = [];
        foreach (TMigrationSet item in items) {
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


    void AttachDate(object entity, bool excluideCreation = false) {
        IHistorySourceSet? historySourceSet = entity as IHistorySourceSet;
        IPivotSourceSet? pivotSourceSet = entity as IPivotSourceSet;
        if (historySourceSet != null) historySourceSet.Timemark = DateTime.Now;
        if (pivotSourceSet != null && !excluideCreation) pivotSourceSet.Creation = DateTime.Now;
    }


    /// <summary>
    /// Perform the navigation changes in a Tmigrationset
    /// </summary>
    //Source.Entry(previousList[i]).CurrentValues.SetValues(newitem);

    void UpdateHelper(ISourceSet current, ISourceSet Record) {
        EntityEntry previousEntry = Source.Entry(current);
        if(previousEntry.State == EntityState.Unchanged) {
            AttachDate(Record, true);
            // Update the non-navigation properties.
            previousEntry.CurrentValues.SetValues(Record);
            foreach (NavigationEntry navigation in previousEntry.Navigations) {
                object? newNavigationValue = Source.Entry(Record).Navigation(navigation.Metadata.Name).CurrentValue;
                // Validate if navigation is a collection.
                if (navigation.CurrentValue is IEnumerable<object> previousCollection && newNavigationValue is IEnumerable<object> newCollection) {
                    List<object> previousList = previousCollection.ToList();
                    List<object> newList = newCollection.ToList();
                    // Perform a search for new items to add in the collection.
                    // NOTE: the followings iterations must be performed in diferent code segments to avoid index length conflicts.
                    for (int i = 0; i < newList.Count; i++) {
                        ISourceSet? newItemSet = (ISourceSet)newList[i];
                        if (newItemSet != null && newItemSet.Id <= 0) {
                            AttachDate(newList[i]);
                            EntityEntry newNavigationEntry = Source.Entry(newList[i]);
                            newNavigationEntry.State = EntityState.Added;
                        }
                    }
                    for (int i = 0; i < previousList.Count; i++) {
                        ISourceSet? previousItem = previousList[i] as ISourceSet;

                        // Find items to modify.
                        // For each new item stored in record collection, will search for an ID match and update the record.
                        foreach (object newitem in newList) {
                            ISourceSet? newItemSet = newitem as ISourceSet;
                            if (previousItem != null && newItemSet != null && previousItem.Id == newItemSet.Id)
                                UpdateHelper(previousItem, newItemSet);
                        }
                    }
                } else if (navigation.CurrentValue == null && newNavigationValue != null) {
                    // Create a new navigation entity.
                    // Also update the attached navigators.
                    AttachDate(newNavigationValue);
                    EntityEntry newNavigationEntry = Source.Entry(newNavigationValue);
                    newNavigationEntry.State = EntityState.Added;
                    navigation.CurrentValue = newNavigationValue;
                } else if (navigation.CurrentValue != null && newNavigationValue != null) {
                    // Update the existing navigation entity

                    ISourceSet? currentItemSet = navigation.CurrentValue as ISourceSet;
                    ISourceSet? newItemSet = newNavigationValue as ISourceSet;

                    if (currentItemSet != null && newItemSet != null) UpdateHelper(currentItemSet, newItemSet);
                }

            }
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Set"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// 
    public async Task<RecordUpdateOut<TMigrationSet>> Update(TMigrationSet Record, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? Include = null) {
        IQueryable<TMigrationSet> query = Set;
        TMigrationSet? old = null;
        TMigrationSet? current;
        if (Include != null) {
           query = Include(query);
        }
        current = await query
            .Where(i => i.Id == Record.Id)
            .FirstOrDefaultAsync();

        if (current != null) {
            Set.Attach(current);
            old = current.DeepCopy();
           UpdateHelper(current, Record);
        } else {
            //Generate a new insert if the given record data not exist.
            AttachDate(Record);
            Set.Update(Record);
        }
         await Source.SaveChangesAsync();

        Disposer?.Push(Source, Record);
        return new RecordUpdateOut<TMigrationSet> {
            Previous = old,
            Updated = current ?? Record,
        };
    }
    #endregion

    #region Delete

    public Task<SourceTransactionOut<TMigrationSet>> Delete(TMigrationSet[] Sets) {

        TMigrationSet[] safe = [];
        SourceTransactionFailure[] fails = [];

        foreach (TMigrationSet set in Sets) {
            try {
                set.EvaluateWrite();
                safe = [.. safe, set];
            } catch (Exception excep) {
                SourceTransactionFailure fail = new(set, excep);
                fails = [.. fails, fail];
            }
        }

        Set.RemoveRange(safe);
        return Task.FromResult<SourceTransactionOut<TMigrationSet>>(new(safe, []));
    }

    public async Task<TMigrationSet> Delete(TMigrationSet Set) {
        Set.EvaluateWrite();
        _ = this.Set.Remove(Set);
        _ = await Source.SaveChangesAsync();
        Source.ChangeTracker.Clear();
        return Set;
    }


    #endregion
}