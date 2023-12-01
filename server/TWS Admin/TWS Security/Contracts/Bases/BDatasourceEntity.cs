using System.ComponentModel.DataAnnotations;

using TWS_Security.Contracts.Interfaces;

namespace TWS_Security.Contracts.Bases;

/// <summary>
///     Represents a inheritance relation between Datasources entity
///     to handle their shared properties such as their pointer to their 
///     datasource position, and another kind of operations.
/// </summary>
/// <typeparam name="TSet"></typeparam>
public abstract class BDatasourceEntity<TSet> : IDatasourceEntity<TSet>
{
    /// <summary>
    ///     Stores the entity identifier into the datasource that provides this entity.
    ///     The setter can only be handled by the entity operations.
    /// </summary>
    [Required]
    public int Pointer { get; protected set; }

    protected BDatasourceEntity()
    {
        Pointer = 0;
    }

    public abstract TSet BuildDatasourceSet();
    public abstract void LoadFromDatasourceSet(TSet Set);
    public abstract bool CompareWithDatasourceSet(TSet Set);
}
