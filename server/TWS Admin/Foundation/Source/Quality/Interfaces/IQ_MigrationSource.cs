namespace CSM_Foundation.Source.Quality.Interfaces;
/// <summary>
///     Interface Quality for [MigrationSource].
///     
///     Defines what quality operations must be performed by a [MigrationSource].
///     
///     [MigrationSource] concept: determines a datasource class mirrored by an Entity Framework
///     migration implementation.
/// </summary>
public interface IQ_MigrationSource {
    /// <summary>
    ///     Qualify if the [MigrationSource] can communicate at runtime.
    /// </summary>
    public void Communication();
}
