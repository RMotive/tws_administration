namespace Foundation.Contracts.Server.Interfaces;

public interface ITemplate<TEstela, TExposure> {
    public Guid Tracer { get; }
    public TEstela Estela { get; }

    public TExposure GenerateExposure();
}
