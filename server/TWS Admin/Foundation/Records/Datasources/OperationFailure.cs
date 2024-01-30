using Foundation.Contracts.Exceptions.Bases;
using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Enumerators.Records;

namespace Foundation.Records.Datasources;
public record OperationFailure<TReference>
    : IFailure {
    public TReference Reference { get; set; }
    public BException Failure { get; set; }
    public OperationFailureCriterias Criteria { get; set; }
    public Type Type { get; set; }

    public OperationFailure(TReference Reference, BException Failure, OperationFailureCriterias Criteria = OperationFailureCriterias.Pointer) {
        this.Reference = Reference;
        this.Failure = Failure;
        this.Criteria = Criteria;
        Type = typeof(TReference);
    }
}