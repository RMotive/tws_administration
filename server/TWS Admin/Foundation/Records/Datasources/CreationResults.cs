using Foundation.Contracts.Datasources.Interfaces;

namespace Foundation.Records.Datasources;
public record CreationResults<TEntity>
    where TEntity : IDatasourceEntity {
    public List<TEntity> Successes { get; set; }

    public List<CreationFailure<TEntity>> Failures { get; set; }

    public CreationResults(List<TEntity> successes, List<CreationFailure<TEntity>> failures) {
        Successes = successes;
        Failures = failures;
    }
}
