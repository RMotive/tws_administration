using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Modelling.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;

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
public abstract class BEntity<TSet, TEntity>
    : BObject<TEntity>, IEntity<TSet>
    where TSet : ISet
    where TEntity : IEntity {
    /// <summary>
    ///     Stores the entity identifier into the datasource that provides this entity.
    ///     The setter can only be handled by the entity operations.
    /// </summary>
    [Required]
    public int Pointer { get; protected set; }

    protected BEntity() {
        Pointer = 0;
    }
    protected abstract Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container);
    protected abstract TSet Generate();
    public abstract bool EqualsSet(TSet Set);
    /// <summary>
    ///     Tries to generate a MUTABLE live datasource set record that is allowed to interact
    ///     with the datasource internal context handler, based on the Entity that calls.
    /// </summary>
    /// <returns>
    ///     <see cref="TSet"/>: A fill-integrity checked live datasource set record representation.
    /// </returns>
    /// <exception cref="XEntityIntegrity{TSet, TEntity}">
    ///     When the integrity check ran over integrity failures.
    /// </exception>
    public TSet GenerateSet() {
        Dictionary<string, IntegrityFailureReasons> integrityFailureReasons = ValidateIntegrity([]);
        if (integrityFailureReasons.Count > 0)
            throw new XEntityIntegrity<TSet, TEntity>(this, integrityFailureReasons);

        return Generate();
    }
}
