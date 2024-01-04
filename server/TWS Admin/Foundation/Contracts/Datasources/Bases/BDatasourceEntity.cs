using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Modelling.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;

using System.ComponentModel.DataAnnotations;

namespace Foundation.Contracts.Datasources.Bases;

/// <summary>
///     Represents an inheritance relation between Datasources entity
///     to handle their shared properties such as their pointer to their 
///     datasource position, and another kind of operations.
///     
///     All entity representation classes should implement their specific constructors,
///     with private setters, and another one constructor that will generate its properties
///     based on a given <typeparamref name="TSet"/>
/// </summary>
/// <typeparam name="TSet">
///     Datasource set entity based.
/// </typeparam>
public abstract class BDatasourceEntity<TSet, TEntity>
    : BObject, IDatasourceEntity<TSet>
    where TSet : IDatasourceSet
    where TEntity : IDatasourceEntity {
    /// <summary>
    ///     Stores the entity identifier into the datasource that provides this entity.
    ///     The setter can only be handled by the entity operations.
    /// </summary>
    [Required]
    public int Pointer { get; protected set; }

    protected BDatasourceEntity() {
        Pointer = 0;
    }

    protected abstract Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container);
    protected abstract TSet GenerateSet();
    public abstract bool EvaluateSet(TSet Set);
    public TSet BuildSet() {
        Dictionary<string, IntegrityFailureReasons> integrityFailureReasons = ValidateIntegrity([]);
        if (integrityFailureReasons.Count > 0)
            throw new XEntityIntegrity<TSet, TEntity>(this, integrityFailureReasons);

        return GenerateSet();
    }
}
