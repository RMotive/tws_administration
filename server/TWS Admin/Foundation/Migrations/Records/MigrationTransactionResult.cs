using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Records;
public record MigrationTransactionResult<TSet, TReference>
    where TSet : IMigrationSet {
    public List<TSet> Successes { get; set; }
    public List<MigrationTransactionFailure> Failures { get; set; }

    public MigrationTransactionResult(List<TSet> Successes, List<MigrationTransactionFailure> Failures) {
        this.Successes = Successes;
        this.Failures = Failures;
    }
}