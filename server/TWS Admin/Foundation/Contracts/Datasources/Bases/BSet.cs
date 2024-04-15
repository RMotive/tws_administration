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
public abstract class BSet<TSet, TEntity, TMigration>
    : BObject<TSet>,
        Interfaces.ISet<TEntity>
    where TEntity : IEntity
    where TSet : ISet
    where TMigration : class {

    #region Properties

    public TMigration? Migration { get; init; } = default!;
    public int Id { get; set; } = default!;

    #endregion

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
        FailureLacks integrityFailureReasons = Validate([]);
        if (integrityFailureReasons.Count > 0)
            throw new XSetIntegrity<TMigration, TSet, TEntity>(this, integrityFailureReasons);

        return Generate();
    }

    public TMigration GenerateMigration() {
        FailureLacks integrityFailureReasons = Validate([]);
        if (integrityFailureReasons.Count > 0)
            throw new XSetIntegrity<TMigration, TSet, TEntity>(this, integrityFailureReasons);

        return Migrate();
    }

    #endregion

    #region Public Abstract Methods

    public abstract bool EqualsEntity(TEntity Entity);

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

    #region Protected Abstract Methods 

    protected abstract TEntity Generate();
    protected abstract TMigration Migrate();

    #endregion
}
