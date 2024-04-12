namespace Foundation.Contracts.Exceptions.Interfaces;
public interface IException<TFailure>
    : IException
    where TFailure : IXFailure {
    public IXFailure GenerateExposure();
    public IException<IXFailure> GenerateDerivation();
}

public interface IException {

}