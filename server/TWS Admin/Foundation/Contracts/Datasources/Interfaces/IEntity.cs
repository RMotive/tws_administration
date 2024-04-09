namespace Foundation.Contracts.Datasources.Interfaces;

/// <summary>
///     Represents a contract to build a Entity class
///     that will represent business entities to work with their data.
/// </summary>
public interface IEntity<TSet>
    : IEntity {
    /// <summary>
    ///     Compares the entity with the given datasource set
    /// </summary>
    /// <returns> If the entity is equal that the datasource set </returns>
    public bool EqualsSet(TSet Set);
    /// <summary>
    ///     Validates if the current entity can be builded into a
    ///     Datasource Set and then will build it.
    /// </summary>
    /// <returns> 
    ///     A Datasource set from the entity.
    /// </returns>
    public TSet GenerateSet();
}

public interface IEntity {
}
