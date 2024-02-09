using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Records.Datasources;

namespace Foundation;

public interface IRepositoryCreate<TEntity, TSet>
    where TEntity : IEntity {

    public Task<TEntity> Create(TEntity Entity);
    public Task<OperationResults<TEntity, TEntity>> Create(List<TEntity> Entities);
    public Task<OperationResults<TEntity, TEntity>> Create(TEntity Entity, int Copies);
}
