using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Modelling.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Datasources;

namespace Foundation.Contracts.Datasources.Bases;
/// <summary>
///     Represents an inheritance relation between Datasource Set to 
///     handle their shared properties and operations.
/// </summary>
public abstract class BDatasourceSet<TSet, TEntity>
    : BObject, IDatasourceSet<TEntity>
    where TEntity : IDatasourceEntity
    where TSet : IDatasourceSet
{
    protected abstract Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container);
    protected abstract TEntity GenerateEntity();
    public abstract bool EvaluateEntity(TEntity Entity);
    public TEntity BuildEntity()
    {
        Dictionary<string, IntegrityFailureReasons> integrityFailureReasons = ValidateIntegrity([]);
        if(integrityFailureReasons.Count > 0) 
            throw new XSetIntegrity<TSet, TEntity>(this, integrityFailureReasons);

        return GenerateEntity();
    }
}
