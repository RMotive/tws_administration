using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions;

namespace Foundation.Records.Datasources;
public record CreationFailure<TEntity> 
    where TEntity : IDatasourceEntity {
    public TEntity Entity { get; set;}
    public BException Failure {  get; set;}

    public CreationFailure(TEntity entity, BException failure) {
        Entity = entity;
        Failure = failure;
    }
}
