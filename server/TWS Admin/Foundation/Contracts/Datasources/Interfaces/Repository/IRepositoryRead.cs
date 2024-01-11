using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Records.Datasources;

namespace Foundation;

public interface IRepositoryRead<TEntity, TSet>
    where TEntity : IEntity
    where TSet : Contracts.Datasources.Interfaces.ISet<TEntity> {
    public Task<TEntity> Read(int Pointer);
    public Task<CriticalOperationResults<TEntity, TSet>> Read(IEnumerable<int> Pointers);
    public Task<CriticalOperationResults<TEntity, TSet>> Read(Predicate<TSet>? Filter = null, ReadingBehavior Behavior = ReadingBehavior.All);
}
