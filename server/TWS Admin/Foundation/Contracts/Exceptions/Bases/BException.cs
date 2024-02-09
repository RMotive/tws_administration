using Foundation.Contracts.Exceptions.Interfaces;

namespace Foundation.Contracts.Exceptions.Bases;
public abstract class BException<TFailure>
    : BException, IException<IExceptionExposure>
    where TFailure : IExceptionExposure {
    protected BException(string Message)
        : base(Message) {

    }

    protected abstract TFailure DesignFailure();

    public IException<IExceptionExposure> GenerateDerivation() {
        return this!;
    }

    public IExceptionExposure GenerateExposure()
    => DesignFailure();
}

public abstract class BException
    : Exception {

    protected BException(string Message)
        : base(Message) {
    }
}