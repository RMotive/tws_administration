namespace Foundation.Contracts.Exceptions.Interfaces;
public interface IException<TFailure>
    : IException
    where TFailure : IExceptionExposure {
    public IExceptionExposure GenerateExposure();
    public IException<IExceptionExposure> GenerateDerivation();
}

public interface IException {

}