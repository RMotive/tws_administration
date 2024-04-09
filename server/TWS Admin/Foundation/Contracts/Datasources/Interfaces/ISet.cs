namespace Foundation.Contracts.Datasources.Interfaces;
public interface ISet<TEntity>
    : ISet
    where TEntity : IEntity {
    public TEntity GenerateEntity();
    public bool EqualsEntity(TEntity Entity);
}

public interface ISet {
    public int Id { get; set; }
}

