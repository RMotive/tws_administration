using Foundation.Contracts.Datasources.Interfaces;

namespace Foundation.Records.Datasources;
public record CriticalOperationResults<TEntity, TSet>
    where TEntity : IEntity
    where TSet : Contracts.Datasources.Interfaces.ISet<TEntity> {
    public List<TEntity> Successes { get; set; }
    public List<OperationFailure<TSet>> Failures { get; set; }
    public int Results { get; set; }
    public int Succeeded { get; set; }
    public int Failed { get; set; }
    public CriticalOperationResults(List<TEntity> successes, List<OperationFailure<TSet>> failures) {
        Successes = successes;
        Failures = failures;
        Succeeded = Successes.Count;
        Failed = Failures.Count;
        Results = Succeeded + Failed;
    }
}