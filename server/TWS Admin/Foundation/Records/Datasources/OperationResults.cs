using Foundation.Contracts.Datasources.Interfaces;

namespace Foundation.Records.Datasources;
public record OperationResults<TEntity, TReference>
    where TEntity : IEntity {
    public List<TEntity> Successes { get; set; }
    public List<OperationFailure<TReference>> Failures { get; set; }

    public OperationResults(List<TEntity> successes, List<OperationFailure<TReference>> failures) {
        Successes = successes;
        Failures = failures;
    }
}