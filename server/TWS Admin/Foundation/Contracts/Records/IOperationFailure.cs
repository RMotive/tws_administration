using Foundation.Contracts.Exceptions.Bases;

namespace Foundation.Contracts.Records;
public interface IOperationFailure<TException>
    : IOperationFailure
    where TException : BException{
    public TException Exception { get; }
}

public interface IOperationFailure { }