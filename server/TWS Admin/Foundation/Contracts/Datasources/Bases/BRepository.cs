using System.Linq.Expressions;
using System.Reflection;

using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;
using Foundation.Records.Datasources;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using OCriteria = Foundation.Enumerators.Records.OperationFailureCriterias;

namespace Foundation.Contracts.Datasources.Bases;
/// <summary>
///     Base contract class that defines the inheritance
///     of a repository behavior.
///     
///     A repository is an object that is responsable of calculate and
///     resolve business logic related to datasource, entities and sets.
/// </summary>
/// <typeparam name="BRepository">
///     The Entity type that this repository will be related to.
/// </typeparam>
/// <typeparam name="TSet">
///     The Set type related to the Entity that is related to this repository implementation.
/// </typeparam>
public abstract class BRepository<TSource, TRepository, TEntity, TSet>
    : IRepository<TEntity, TSet>
    where TRepository : IRepository
    where TEntity : BEntity<TSet, TEntity>
    where TSet : BSet<TSet, TEntity>, ISet, new()
    where TSource : DbContext {
    /// <summary>
    ///     Internal repository datasource context handler
    /// </summary>
    protected readonly TSource Live;
    /// <summary>
    ///     Internal datasource interface set handler.
    /// </summary>
    protected readonly DbSet<TSet> Set;
    /// <summary>
    ///     Instances the Base repository manager instance.
    ///        
    ///     It handles and performs several managing things to implement
    ///     and resolve a repository implementation functionallity.
    /// </summary>
    /// <param name="Live">
    ///     Datasource handler instance to use in this datasource repository lifetime context.
    /// </param>
    protected BRepository(TSource Live) {
        this.Live = Live;
        Set = this.Live.Set<TSet>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReference"></typeparam>
    /// <param name="References"></param>
    /// <param name="Delegator"></param>
    /// <param name="Criteria"></param>
    /// <returns></returns>
    private static async Task<OperationResults<TEntity, TReference>> IterateOperation<TReference>(IEnumerable<TReference> References, Func<TReference, Task<TEntity>> Delegator, OCriteria Criteria = OCriteria.Pointer) {
        List<TEntity> Successes = [];
        List<OperationFailure<TReference>> Failures = [];
        foreach (TReference Ref in References) {
            try {
                TEntity Success = await Delegator(Ref);
                Successes.Add(Success);
            } catch (BException X) {
                OperationFailure<TReference> Failure = new(Ref, X, Criteria);
                Failures.Add(Failure);
            }
        }
        OperationResults<TEntity, TReference> Results = new(Successes, Failures);
        return Results;
    }
    /// <summary>
    ///     Creates a new datasource Set record in the live datasource store.
    /// </summary>
    /// <param name="Entity">
    ///     Entity to store.
    /// </param>
    /// <returns>
    ///     The Entity updated with the new auto-generated and fixed data.
    /// </returns>
    /// <exception cref="XUniqueViolation{TSet}">
    ///     If the datasource set has unique values on its columns and the Entity requested
    ///     to be saved violates this unique constraints.
    /// </exception>
    public async Task<TEntity> Create(TEntity Entity) {
        TSet Record = Entity.GenerateSet();
        try {
            await Live.AddAsync(Record);
            await Live.SaveChangesAsync();
            TEntity Saved = Record.GenerateEntity();
            return Saved;
        } catch (DbUpdateException X)
            // <-- DEV NOTE: Notice that here we're just looking for a specific ORM source manager exception when unique keys conflict, 
            // but not the exceptions that can be thrown by GenerateSet and GenerateEntity, that exceptions should be managed directly 
            // by the middleware or another top-level statement that requires that cause both of them ARE INTEGRITY SAFE PROCESS OPERATION BREAKERS
            when (X.InnerException is SqlException) {
            Live.Remove(Record);
            PropertyInfo[] SetProperties = typeof(TSet).GetProperties();
            List<TSet> UpSets = await Set.AsNoTracking().ToListAsync();
            List<PropertyInfo> Violations = [];
            foreach (PropertyInfo SetProperty in SetProperties) {
                IEnumerable<CustomAttributeData> CustomAttributes = SetProperty.CustomAttributes;
                bool IsUnique = CustomAttributes
                    .Any(i => i.AttributeType == typeof(UniqueAttribute));
                if (!IsUnique) continue;

                object? SavingSetValue = SetProperty.GetValue(Record);
                foreach (TSet UpSet in UpSets) {
                    object? UpSetValue = SetProperty.GetValue(UpSet);
                    if (SavingSetValue == UpSetValue) {
                        Violations.Add(SetProperty);
                        break;
                    }
                }
            }

            throw new XUniqueViolation<TSet>(Violations);
        }
    }
    /// <summary>
    ///     Tries to save every given Entity.
    /// </summary>
    /// <param name="Entities">
    ///     Collection of Entities to Create.
    /// </param>
    /// <returns>
    ///     <see cref="OperationResults{TEntity, TEntity}"/>: That contains the collection of Entities successfuly saved and Failures caught during the creation proccess.
    /// </returns>
    public async Task<OperationResults<TEntity, TEntity>> Create(List<TEntity> Entities)
    => await IterateOperation(Entities, Create, OCriteria.Entity);
    /// <summary>
    ///     Tries to save several copies of the same Entity into the live datasource.
    /// </summary>
    /// <param name="Entity">
    ///     The Entity that will be saved.
    /// </param>
    /// <param name="Copies">
    ///     The number of copies to save.
    /// </param>
    /// <returns> 
    ///     <see cref="OperationResults{TEntity, TEntity}"/>: A bundle of the creation results, it has the collection of success and failures resolved during the creation proccess.
    /// </returns>
    public async Task<OperationResults<TEntity, TEntity>> Create(TEntity Entity, int Copies)
    => await IterateOperation(Enumerable.Repeat(Entity, Copies), Create, OCriteria.Entity);
    /// <summary>
    ///     Reads a specific record from the live database set (<see cref="TSet"/>) on the repository context (<see cref="TRepository"/>).
    /// </summary>
    /// <param name="Pointer">
    ///     The specific identifier that points the record into the live database set.
    /// </param>
    /// <returns>
    ///     <see cref="TEntity"/>: Builded Entity based on the record fetched.
    /// </returns>
    /// <exception cref="XRecordUnfound{TRepository}">
    ///     When the 
    /// </exception>
    public async Task<TEntity> Read(int Pointer) {
        TSet Record = await Set
            .Where(I => I.Id == Pointer)
            .FirstOrDefaultAsync()
        ?? throw new XRecordUnfound<TRepository>(nameof(Read), Pointer, RecordSearchMode.ByPointer);

        TEntity Entity = Record.GenerateEntity();
        return Entity;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Pointers"></param>
    /// <returns></returns>
    public async Task<CriticalOperationResults<TEntity, TSet>> Read(IEnumerable<int> Pointers) {
        List<TEntity> Successes = [];
        List<OperationFailure<TSet>> Failures = [];
        foreach (int Pointer in Pointers) {
            try {
                TEntity Success = await Read(Pointer);
                Successes.Add(Success);
            } catch (XRecordUnfound<TRepository> X) {
                TSet UnfoundRecords = new() {
                    Id = Pointer,
                };
                OperationFailure<TSet> Failure = new(UnfoundRecords, X);
                Failures.Add(Failure);
            } catch (BException X) {
                TSet FoundRecord = await Set
                    .AsNoTracking()
                    .Where(I => I.Id == Pointer).FirstAsync();
                OperationFailure<TSet> Failure = new(FoundRecord, X);
                Failures.Add(Failure);
            }
        }
        CriticalOperationResults<TEntity, TSet> Results = new(Successes, Failures);
        return Results;
    }
    /// <summary>
    ///     Fetches all the records into the live database set, after that they will get 
    ///     validated about all the specific properties boundries specified and all the
    ///     successes and failures will be stored to resolve.
    ///     
    ///     <para>
    ///         <para> Why is this a critical operation?  </para>
    ///         R: This is considerer as a critical operation cause the live database can have
    ///         a DBA that could be inserting data directly through queries to the live database sets,
    ///         and to avoid misstake one of them to be validated, the Read operation passes all of the
    ///         fetched records throughout a validation step and detect all of that records are not valid
    ///         to be used and proccesses along the user and system interaction. :) 
    ///     </para>
    /// </summary>
    /// <param name="Filter">
    ///     Indicates a specific matching patter that the Set must match to be considered as the
    ///     resulted records.
    /// </param>
    /// <param name="Behavior">
    ///     Indicates the Reading behavior, for both proccesses with filter and without filter,
    ///     By default it will return all results found.
    /// </param>
    /// <returns>
    ///     <see cref="CriticalOperationResults{TSet, TEntity}"/>: The operation resolution,
    ///     it contains internally a collection of all of that records that could be validated correclty
    ///     into entities and a collection of all records that failed during validations, with the Exception
    ///     catched and the Set retrieved from the live database.
    /// </returns>
    /// <exception cref="XRecordUnfound{TRepository}">
    ///     Thrown when the operation can't found any record.
    /// </exception>
    public async Task<CriticalOperationResults<TEntity, TSet>> Read(Expression<Predicate<TSet>>? Filter = null, ReadingBehavior Behavior = ReadingBehavior.All) {
        List<TSet> Records = [];
        // --> Filter behavior
        if (Filter is null)
            Records = await Set.ToListAsync();
        else
            Records = Set
                .AsNoTracking()
                .AsEnumerable()
                .Where((I) => Filter.Compile().Invoke(I))
                .ToList();

        if (Records.Count == 0)
            throw new XRecordUnfound<TRepository>(nameof(Read), $"{Filter?.Body} | {Behavior}", RecordSearchMode.ByMatch);
        // --> Reading behavior
        Records = Behavior switch {
            ReadingBehavior.First => [Records[0]],
            ReadingBehavior.Last => [Records[^1]],
            _ => Records
        };

        // --> Building resolution
        List<TEntity> Successes = [];
        List<OperationFailure<TSet>> Failures = [];
        foreach (TSet Record in Records) {
            try {
                TEntity Success = Record.GenerateEntity();
                Successes.Add(Success);
            } catch (BException X) {
                OperationFailure<TSet> Failure = new(Record, X, OCriteria.Set);
                Failures.Add(Failure);
            }
        }
        CriticalOperationResults<TEntity, TSet> Results = new(Successes, Failures);
        return Results;
    }
    /// <summary>
    ///     Tries to update an existing live database set records based on the given 
    ///     Entity properties. 
    ///     It will search if there exist a record with the given Entity pointr and only
    ///     will update it if the records already exist (To avoid this behavior set <see cref="Fallback"/> property to true).
    /// </summary>
    /// <param name="TEntity">
    ///     The Entity properties to handle the updating.
    /// </param>
    /// <param name="Fallback">
    ///     Indicates wheter the operation should use its fallback or not.
    ///     true: If the record isn't found then it will be saved with a auto-generated Pointer.
    ///     false: If the record isn't found then the operation will be cancelled and will throw an exception.
    /// </param>
    /// <returns>
    ///     The full-integrity validated Entity with the most recent and validated live datasource set record data.
    /// </returns>
    /// <exception cref="XRecordUnfound{TRepository}">
    ///     When the record to update doesn't exist.
    /// </exception>
    public async Task<TEntity> Update(TEntity TEntity, bool Fallback = false) {
        TSet Rec = TEntity.GenerateSet();
        bool Exist = await Set
            .AsNoTracking()
            .Where(I => I.Id == Rec.Id).AnyAsync();

        if (!Exist && !Fallback)
            throw new XRecordUnfound<TRepository>(nameof(Update), TEntity, RecordSearchMode.ByPointer);
        if (!Exist && Fallback) {
            Rec.Id = 0;
            await Set.AddAsync(Rec);
        } else
            Set.Update(Rec);

        await Live.SaveChangesAsync();
        TEntity Ety = Rec.GenerateEntity();
        Live.ChangeTracker.Clear();
        return Ety;
    }

    public async Task<TEntity> Delete(TEntity Entity) {
        TSet Rec = Entity.GenerateSet();

        Live.Remove(Rec);
        await Live.SaveChangesAsync();
        Live.ChangeTracker.Clear();

        return Entity;
    }
}
