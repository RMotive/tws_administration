using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Modelling.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;

namespace Foundation.Contracts.Datasources.Bases;
/// <summary>
///     Represents an inheritance relation between Datasource Set to 
///     handle their shared properties and operations.
/// </summary>
public abstract class BSet<TSet, TEntity>
    : BObject<TSet>, 
        Interfaces.ISet<TEntity>
    where TEntity : IEntity
    where TSet : ISet {
    public abstract int Id { get; set; }
    
    protected abstract Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container);
    protected abstract TEntity Generate();
    public abstract bool EqualsEntity(TEntity Entity);
    /// <summary>
    ///     Tries to generate a integrity full-checked Entity based on the
    ///     live datasource set record that performs the action.
    /// </summary>
    /// <returns>
    ///     <see cref="TEntity"/>:The inmutable integrity full-checked Entity, to safe-handling data.
    /// </returns>
    /// <exception cref="XSetIntegrity{TSet, TEntity}">
    ///     If the integrity check ran over integrity failures.
    /// </exception>
    public TEntity GenerateEntity() {
        Dictionary<string, IntegrityFailureReasons> integrityFailureReasons = ValidateIntegrity([]);
        if (integrityFailureReasons.Count > 0)
            throw new XSetIntegrity<TSet, TEntity>(this, integrityFailureReasons);

        return Generate();
    }
}
