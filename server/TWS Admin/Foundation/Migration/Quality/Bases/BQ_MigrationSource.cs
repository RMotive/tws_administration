using Foundation.Migrations.Bases;
using Foundation.Migrations.Quality.Interfaces;

using Xunit;

namespace Foundation.Migrations.Quality.Bases;

/// <summary>
///     Base Quality for [MigrationSource].
///     
///     Define standard behaviors and quality checks for [MigrationSource] concept.
///     
///     [MigrationSource] concept: determines a datasource class mirrored by an Entity Framework
///     migration implementation.
/// </summary>
/// <typeparam name="TSource">
///     Type of the [MigrationSource] implementation class.
/// </typeparam>
public abstract class BQ_MigrationSource<TSource>
    : IQ_MigrationSource
    where TSource : BMigrationSource<TSource> {
    /// <summary>
    ///     EF [MigrationSource]. 
    /// </summary>
    protected readonly TSource Source;

    /// <summary>
    ///     Generates a new base quality class for [MigrationSource].
    /// </summary>
    /// <param name="Source"></param>
    public BQ_MigrationSource(TSource Source) {
        this.Source = Source;
    }

    [Fact]
    public void Communication() {
        Assert.True(Source.Database.CanConnect(), $"{GetType()} cannot connect, check your connection credentials");
    }

    [Fact]
    public void Evaluate() {
        Source.Evaluate();
    }
}
