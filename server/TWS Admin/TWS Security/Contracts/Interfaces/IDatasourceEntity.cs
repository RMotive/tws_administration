namespace TWS_Security.Contracts.Interfaces;

/// <summary>
///     Represents a contract to build a Entity class
///     that will represent business entities to work with their data.
/// </summary>
public interface IDatasourceEntity<TSet>
{
    /// <summary>
    ///     Compares the entity with the given datasource set
    /// </summary>
    /// <returns> If the entity is equal that the datasource set </returns>
    public bool CompareWithDatasourceSet(TSet Set);
    /// <summary>
    ///     Loads the current entity object data based on a
    ///     given Datasource set that is the mirroring of the entity.
    /// </summary>
    /// <param name="Set"> Datasource Set entity reflection </param>
    public void LoadFromDatasourceSet(TSet Set);
    /// <summary>
    ///     Validates if the current entity can be builded into a
    ///     Datasource Set and then will build it.
    /// </summary>
    /// <returns> 
    ///     A Datasource set from the entity.
    /// </returns>
    public TSet BuildDatasourceSet();
}
