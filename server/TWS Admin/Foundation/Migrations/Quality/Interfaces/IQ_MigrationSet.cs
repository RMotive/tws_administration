using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Quality.Interfaces;

/// <summary>
///     Interface Quality for [Q_Entity].
///     
///     Defines what quality operations must be performed by a [Q_Entity].
///     
///     [Q_Entity] concept: determines a quality implementation to qualify 
///     a [MigrationSource] [Entity] implementation.
/// </summary>
public interface IQ_MigrationSet {
    /// <summary>
    ///     Qualifies:
    ///         - [Entity] Evaluate success.
    ///         - [Entity] Evaluate fails.
    /// </summary>
    public void Evaluate();
}
