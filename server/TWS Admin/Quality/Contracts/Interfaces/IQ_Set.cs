namespace TWS_Security.Quality.Contracts.Interfaces;

/// <summary>
///     Interface for [Q_Set].
///     
///     Defines required tests methods to be performed from [Q_Set]'s.
///     
///     [Q_Set] concept: a quality class to evaluate Set concept implementations. 
/// </summary>
public interface IQ_Set {
    /// <summary>
    ///     Tests Entity generation from a Set.
    /// </summary>
    public void BuildSet();
    /// <summary>
    ///     Tests the Set equality with an Entity.
    /// </summary>
    public void EvaluateSet();
}
