using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Records;
public record MigrationTransactionResult<TSet>
    where TSet : IMigrationSet {
    public TSet[] Successes { get; init; }
    public MigrationTransactionFailure[] Failures { get; init; }
    public int QTransactions { get; private set; }
    public int QSuccesses { get; private set; }
    public int QFailures { get; private set; }
    public bool Failed { get; private set; }

    public MigrationTransactionResult(TSet[] Successes, MigrationTransactionFailure[] Failures) {
        this.Successes = Successes;
        this.Failures = Failures;
        this.QSuccesses = this.Successes.Length;
        this.QFailures = this.Failures.Length;
        QTransactions = QSuccesses + QFailures;
        Failed = QFailures > 0;
    }
}