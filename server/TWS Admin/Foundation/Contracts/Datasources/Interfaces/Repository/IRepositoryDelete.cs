using Foundation.Contracts.Datasources.Interfaces;

namespace Foundation;

public interface IRepositoryDelete<TEntity, TSet> 
    where TEntity : IEntity
    where TSet : ISet {
    
    public Task<TEntity> Delete(TEntity Entity);
}
