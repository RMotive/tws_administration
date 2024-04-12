namespace Foundation.Contracts.Server.Interfaces;
public interface IServing<TEstela> {
    public Guid Tracer { get; }
    public TEstela Estela { get; }
}
