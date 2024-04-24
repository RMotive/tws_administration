using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Records;
public record MigrationTransactionFailure {
    required public IMigrationSet Set { get; init; }
    required public Exception System { get; init; }
}