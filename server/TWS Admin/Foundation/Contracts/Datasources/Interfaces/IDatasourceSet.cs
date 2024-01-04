namespace Foundation.Contracts.Datasources.Interfaces;
public interface IDatasourceSet<TEntity>
    : IDatasourceSet
    where TEntity : IDatasourceEntity {
    public TEntity BuildEntity();
    public bool EvaluateEntity(TEntity Entity);
}

public interface IDatasourceSet {
}

