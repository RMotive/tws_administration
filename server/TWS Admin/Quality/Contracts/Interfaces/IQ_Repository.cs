namespace TWS_Security.Quality.Contracts.Interfaces;
/// <summary>
///     Interface for [Q_Repository].
///     
///     Defines required tests methods to be performed from [Q_Repository]'s.
///     
///     [Q_Repository] concept: a quality class to evaluate Repository concept implementations. 
/// </summary>
public interface IQ_Repository {
    /// <summary>
    ///     Tests Repository Create implementations.
    /// </summary>
    public void Create();
    /// <summary>
    ///     Tests Repository Read implementations.
    /// </summary>
    public void Read();
    /// <summary>
    ///     Tests Repository Update implementations.
    /// </summary>
    public void Update();
    /// <summary>
    ///     Tests Repository Delete implementations.
    /// </summary>
    public void Delete();

}
