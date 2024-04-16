using System.Linq.Expressions;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Migrations.Records;

namespace Foundation.Migrations.Interfaces.Depot;

public interface IRepositoryRead<TEntity, TSet>
    where TEntity : IEntity
    where TSet : Contracts.Datasources.Interfaces.ISet<TEntity> {
    public Task<TEntity> Read(int Pointer);
    public Task<MigrationTransactionResult_Critical<TEntity, TSet>> Read(IEnumerable<int> Pointers);
    public Task<MigrationTransactionResult_Critical<TEntity, TSet>> Read(Expression<Predicate<TSet>>? Filter = null, ReadingBehavior Behavior = ReadingBehavior.All);
}
