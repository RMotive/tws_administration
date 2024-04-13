using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Quality.Records;

using Xunit;

namespace Foundation.Migrations.Quality.Bases;
/// <summary>
///     Base Quality for [Q_Entity].
///     
///     Defines what quality operations must be performed by a [Q_Entity].
///     
///     [Q_Entity] concept: determines a quality implementation to qualify 
///     a [MigrationSource] [Entity] implementation.
/// </summary>
public abstract class BQ_MigrationSet<TSet>
    where TSet : IMigrationSet, new() {

    protected abstract Q_MigrationSet_EvaluateRecord<TSet>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<TSet>[] Container);

    [Fact]
    public void Definition() {
        TSet mock = new();
        mock.Evaluate(true);
    }

    [Fact]
    public void Evaluate() {
        Q_MigrationSet_EvaluateRecord<TSet>[] expectations = EvaluateFactory([]);


        foreach (Q_MigrationSet_EvaluateRecord<TSet> expectation in expectations) {
            TSet mock = expectation.Mock;

            mock.Evaluate();
        }
    }
}
