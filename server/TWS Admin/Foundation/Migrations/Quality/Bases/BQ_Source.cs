using Foundation.Migrations.Quality.Interfaces;

using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Foundation.Migrations.Quality.Bases;

/// <summary>
///     Base Quality for [Source].
///     
///     Define standard behaviors and quality checks for [Source] concept.
///     
///     [Source] concept: determines a datasource class mirrored by an Entity Framework
///     migration implementation.
/// </summary>
/// <typeparam name="TSource">
///     Type of the [Source] implementation class.
/// </typeparam>
public abstract class BQ_Source<TSource>
    : IQ_Source
    where TSource : DbContext {
    /// <summary>
    ///     EF [Source]. 
    /// </summary>
    protected readonly TSource Source;

    /// <summary>
    ///     Generates a new base quality class for [Source].
    /// </summary>
    /// <param name="Source"></param>
    public BQ_Source(TSource Source) {
        this.Source = Source;
    }

    [Fact]
    public void Communication() {
        Assert.True(Source.Database.CanConnect());
    }
}
