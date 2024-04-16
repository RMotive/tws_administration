using Foundation.Contracts.Exceptions.Bases;
using Foundation.Contracts.Records;
using Foundation.Enumerators.Records;
using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Records;
public record MigrationTransactionFailure
    : IOperationFailure {
    public IMigrationSet Reference { get; set; }
    public BException Failure { get; set; }
    public OperationFailureCriterias Criteria { get; set; }
    public Type Type { get; set; }

    public MigrationTransactionFailure(IMigrationSet Reference, BException Failure, OperationFailureCriterias Criteria = OperationFailureCriterias.Pointer) {
        this.Reference = Reference;
        this.Failure = Failure;
        this.Criteria = Criteria;
        Type = Reference.GetType();
    }
}