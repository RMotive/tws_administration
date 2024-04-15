using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Quality.Records;
public record Q_MigrationSet_EvaluateRecord<TSet>
    where TSet : IMigrationSet {

    public (string, (IValidator, int)[])[] Expectations { get; init; } = [];
    public TSet Mock { get; init; } = default!;

    public Q_MigrationSet_EvaluateRecord() { }

    public Q_MigrationSet_EvaluateRecord((string, (IValidator, int)[])[] Expectations, TSet Mock) {
        this.Expectations = Expectations;
        this.Mock = Mock;
    }
}
