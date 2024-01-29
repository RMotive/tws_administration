namespace Server.Contracts.Interfaces;

public interface ITEmplate<TEstela> {
    public Guid Tracer { get; }
    public TEstela Estela { get; }
}
