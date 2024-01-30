namespace Foundation.Contracts.Exceptions.Interfaces;
public interface IFailure<TException>
    : IFailure
    where TException : IException {
}

public interface IFailure { }
