namespace Customer.Contracts.Interfaces;
public interface IBusinessService<TEntity>
{
    /// <summary>
    ///     Fetches all entities related with the service
    /// </summary>
    /// <returns> The entity collection of the service context </returns>
    public List<TEntity> All();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TEntity FromId(int id);
}
