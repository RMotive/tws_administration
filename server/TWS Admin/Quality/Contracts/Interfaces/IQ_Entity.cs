namespace TWS_Security.Quality.Contracts.Interfaces;

/// <summary>
///     Interface for [Q_Entity].
///     
///     Defines required tests methods to be performed from [Q_Entity]'s.
///     
///     [Q_Entity] concept: a quality class to evaluate Entity concept implementations. 
/// </summary>
public interface IQ_Entity {
    /// <summary>
    ///     Tests Set generation from an Entity.
    /// </summary>
    public void BuildSet();
    /// <summary>
    ///     Tests the Entity equality with a Set.
    /// </summary>
    public void EvaluateSet();
}
