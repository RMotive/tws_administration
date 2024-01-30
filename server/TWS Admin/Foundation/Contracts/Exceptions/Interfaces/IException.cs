namespace Foundation.Contracts.Exceptions.Interfaces;
public interface IException<TFailure>
    : IException
    where TFailure : IFailure {
    public TFailure GenerateFailure();
}

public interface IException {

}