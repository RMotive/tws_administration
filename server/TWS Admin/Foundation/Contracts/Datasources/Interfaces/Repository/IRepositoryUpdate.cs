using Foundation.Contracts.Datasources.Interfaces;

namespace Foundation;

public interface IRepositoryUpdate<TEntity, TSet>
    where TEntity : IEntity
    where TSet : ISet {

    public Task<TEntity> Update(TEntity TEntity, bool Fallback = false);
}
