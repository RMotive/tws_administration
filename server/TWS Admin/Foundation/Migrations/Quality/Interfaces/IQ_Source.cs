namespace Foundation.Migrations.Quality.Interfaces;
/// <summary>
///     Interface Quality for [Source].
///     
///     Defines what quality operations must be performed by a [Source].
///     
///     [Source] concept: determines a datasource class mirrored by an Entity Framework
///     migration implementation.
/// </summary>
public interface IQ_Source {
    /// <summary>
    ///     Qualify if the [Source] can communicate at runtime.
    /// </summary>
    public void Communication();
}
