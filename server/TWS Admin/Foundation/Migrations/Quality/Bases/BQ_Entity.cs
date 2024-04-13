using System.Reflection;

using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Quality.Interfaces;

using Xunit;

namespace Foundation.Migrations.Quality.Bases;
/// <summary>
///     Base Quality for [Q_Entity].
///     
///     Defines what quality operations must be performed by a [Q_Entity].
///     
///     [Q_Entity] concept: determines a quality implementation to qualify 
///     a [Source] [Entity] implementation.
/// </summary>
public abstract class BQ_Entity<TEntity, TSet> 
    : IQ_Entity
    where TEntity : IMigrationEntity<TSet>
    where TSet : class, new() {

    protected abstract Dictionary<PropertyInfo[], TEntity> EvaluateFactory();

    [Fact]
    public void Evaluate() {
        Dictionary<PropertyInfo[], TEntity> factory = EvaluateFactory();

        foreach (KeyValuePair<PropertyInfo[], TEntity> expec in factory) {

        }
    }
}
