using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Modelling.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;

using FailureLacks = System.Collections.Generic.Dictionary<string, Foundation.Enumerators.Exceptions.IntegrityFailureReasons>;

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


    protected abstract TEntity Generate();
    public abstract bool EqualsEntity(TEntity Entity);

    #region Private Methods

    /// <summary>
    ///     Internally validates managed properties along sub-classes and calls the Set implementation
    ///     <see cref="ValidateIntegrity(Dictionary{string, IntegrityFailureReasons})"/> method to resolve the internal validation.
    /// </summary>
    /// <param name="Container"></param>
    /// <returns></returns>
    private Dictionary<string, IntegrityFailureReasons> Validate(Dictionary<string, IntegrityFailureReasons> Container) {
        if (Id <= 0)
            Container.Add(nameof(Id), IntegrityFailureReasons.LessOrEqualZero);
        return ValidateIntegrity(Container);
    }

    #endregion

    #region Public Methods

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
        Dictionary<string, IntegrityFailureReasons> integrityFailureReasons = Validate([]);
        if (integrityFailureReasons.Count > 0)
            throw new XSetIntegrity<TSet, TEntity>(this, integrityFailureReasons);

        return Generate();
    }

    #endregion

    #region Protected Methods

    /// <summary>
    ///     Validates the integrity of <see cref="TSet"/>.
    ///     
    ///     This collects possible integrity lacks along transaction between user -> server -> database, and database -> server -> user
    /// </summary>
    /// <param name="Container">
    ///     Container to store the integrity lacks by property name and reason.
    /// </param>
    /// <returns>
    ///      The Container of integrity lacks calculated by the Entity instance.
    /// </returns>
    protected abstract FailureLacks ValidateIntegrity(FailureLacks Container);

    #endregion
}
