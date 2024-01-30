namespace Foundation.Contracts.Server.Interfaces;

public interface ITEmplate<TEstela, TExposure> {
    public Guid Tracer { get; }
    public TEstela Estela { get; }

    public TExposure GenerateExposure();
}
