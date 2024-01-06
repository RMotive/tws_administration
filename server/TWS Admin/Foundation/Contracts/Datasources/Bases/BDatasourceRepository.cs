using System.Reflection;

using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions;
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
/// <typeparam name="TEntity">
///     The Entity type that this repository will be related to.
/// </typeparam>
/// <typeparam name="TSet">
///     The Set type related to the Entity that is related to this repository implementation.
/// </typeparam>
public abstract class BDatasourceRepository<TRepository, TEntity, TSet, TSource>
    : IDatasourceRepository<TEntity, TSet>
    where TRepository : IDatasourceRepository
    where TEntity : IDatasourceEntity<TSet>
    where TSet : class, IDatasourceSet<TEntity>
    where TSource : DbContext {
    /// <summary>
    ///     Internal repository datasource context handler
    /// </summary>
    protected readonly TSource _Source;
    /// <summary>
    ///     Instances the Base repository manager instance.
    ///        
    ///     It handles and performs several managing things to implement
    ///     and resolve a repository implementation functionallity.
    /// </summary>
    /// <param name="Source">
    ///     Datasource handler instance to use in this datasource repository lifetime context.
    /// </param>
    protected BDatasourceRepository(TSource Source) {
        _Source = Source;
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
    /// <exception cref="XUniqueViolation">
    ///     If the datasource set has unique values on its columns and the Entity requested
    ///     to be saved violates this unique constraints.
    /// </exception>
    public async Task<TEntity> Create(TEntity Entity) {
        TSet Set = Entity.BuildSet();
        try {
            await _Source.AddAsync(Set);
            await _Source.SaveChangesAsync();
            TEntity Saved = Set.BuildEntity();
            return Saved;
        } catch (DbUpdateException X)
            when (X.InnerException is SqlException) {

            _Source.Remove(Set);
            PropertyInfo[] SetProperties = typeof(TSet).GetProperties();
            List<TSet> UpSets = await _Source.Set<TSet>().AsNoTracking().ToListAsync();
            List<PropertyInfo> Violations = [];
            foreach (PropertyInfo SetProperty in SetProperties) {
                IEnumerable<CustomAttributeData> CustomAttributes = SetProperty.CustomAttributes;
                bool IsUnique = CustomAttributes
                    .Any(i => i.AttributeType == typeof(UniqueAttribute));
                if (!IsUnique) continue;

                object? SavingSetValue = SetProperty.GetValue(Set);
                foreach (TSet UpSet in UpSets) {
                    object? UpSetValue = SetProperty.GetValue(UpSet);
                    if (SavingSetValue == UpSetValue) {
                        Violations.Add(SetProperty);
                        break;
                    }
                }
            }

            throw new XUniqueViolation(typeof(TSet), Violations);
        }
    }
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
    ///     <see cref="OperationResultsEntity{TEntity}"/>: A bundle of the creation results, it has the collection of success and failures resolved during the creation proccess.
    /// </returns>
    public async Task<OperationResults<TEntity, TEntity>> Create(TEntity Entity, int Copies) {
        List<TEntity> Successes = [];
        List<OperationFailure<TEntity>> Failures = [];
        for (int Pointing = 0; Pointing < Copies; Pointing++) {
            try {
                TEntity CreatedEntity = await Create(Entity);
                Successes.Add(CreatedEntity);
            } catch (BException X) {
                OperationFailure<TEntity> Failure = new(Entity, X, OCriteria.Entity);
                Failures.Add(Failure);
            }
        }

        OperationResults<TEntity, TEntity> CreationResults = new(Successes, Failures);
        return CreationResults;
    }
    /// <summary>
    ///     Tries to save every given Entity.
    /// </summary>
    /// <param name="Entities">
    ///     Collection of Entities to Create.
    /// </param>
    /// <returns>
    ///     <see cref="OperationResultsEntity{TEntity}"/>: That contains the collection of Entities successfuly saved and Failures caught during the creation proccess.
    /// </returns>
    public async Task<OperationResults<TEntity, TEntity>> Create(List<TEntity> Entities) {
        List<TEntity> Successes = [];
        List<OperationFailure<TEntity>> Failures = [];

        foreach (TEntity Entity in Entities) {
            try {
                TEntity Success = await Create(Entity);
                Successes.Add(Success);
            } catch (BException X) {
                OperationFailure<TEntity> Failure = new(Entity, X, OCriteria.Entity);
                Failures.Add(Failure);
            }
        }
        OperationResults<TEntity, TEntity> CreationResults = new(Successes, Failures);
        return CreationResults;
    }

    public Task<TEntity> Read(int Pointer) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Read() {
        throw new NotImplementedException();
    }

    public Task<OperationResults<TEntity, int>> Read(List<int> Pointers) {
        throw new NotImplementedException();
    }

    public Task<OperationResults<TEntity, Predicate<TSet>>> Read(Predicate<TSet> Match, bool FirstOnly = false) {
        throw new NotImplementedException();
    }

    public Task<TEntity> Update(TEntity Entity) {
        throw new NotImplementedException();
    }

    public Task<TEntity> Update(int Pointer, TEntity Entity) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Update(List<TEntity> Entities) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Update(List<int> Pointers, List<TEntity> Entities) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Update(List<int> Pointers, TEntity Entity) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Update(Predicate<TEntity> Match, Func<TEntity, TEntity> Refactor, bool FirstOnlt = false) {
        throw new NotImplementedException();
    }

    public Task<TEntity> Delete(TEntity Entity) {
        throw new NotImplementedException();
    }

    public Task<TEntity> Delete(int Pointer) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Delete(List<TEntity> Entities) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Delete(List<int> Entities) {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> Delete(Predicate<TEntity> Match, bool FirstOnly = false) {
        throw new NotImplementedException();
    }
}
