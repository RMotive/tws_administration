using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Foundation.Contracts.Exceptions.Bases;
public abstract class BException<TFailure>
    : BException, IException<IXFailure>
    where TFailure : IXFailure {
    protected BException(string message, Situation? situation = null)
        : base(message, situation) {

    }
    protected abstract TFailure DesignFailure();
    public IException<IXFailure> GenerateDerivation() {
        return this!;
    }

    public IXFailure GenerateExposure()
    => DesignFailure();
}

public abstract class BException
    : Exception {
    public Situation Situation { get; set; }
    protected BException(string Message, Situation? situation = null)
        : base(Message) {
        Situation = situation ?? new Situation(0, "Unset");
    }
    public virtual Dictionary<string, dynamic> GenerateAdvising()
    => [];
}