namespace Foundation.Contracts.Datasources.Interfaces;
public interface IDatasourceRepository<TEntity, TSet>
    where TEntity : IDatasourceEntity
    where TSet : IDatasourceSet
{
    // --> Create interfaces <--
    public TEntity Create(TEntity Entity);
    public List<TEntity> Create(TEntity Entity, int Copies);
    public List<TEntity> Create(List<TEntity> Entities);
    // --> Read interfaces <--
    public TEntity Read(int Pointer);
    public List<TEntity> Read();
    public List<TEntity> Read(List<int> Pointers);
    public List<TEntity> Read(Predicate<TSet> Match, bool FirstOnly = false);
    // --> Update interfaces <--
    public TEntity Update(TEntity Entity);
    public TEntity Update(int Pointer, TEntity Entity);
    public List<TEntity> Update(List<TEntity> Entities);
    public List<TEntity> Update(List<int> Pointers, List<TEntity> Entities);
    public List<TEntity> Update(List<int> Pointers, TEntity Entity);
    public List<TEntity> Update(Predicate<TEntity> Match, Func<TEntity, TEntity> Refactor, bool FirstOnlt = false);
    // --> Delete interfaces <--
    public TEntity Delete(TEntity Entity);
    public TEntity Delete(int Pointer);
    public List<TEntity> Delete(List<TEntity> Entities);
    public List<TEntity> Delete(List<int> Entities);
    public List<TEntity> Delete(Predicate<TEntity> Match, bool FirstOnly = false);
}
