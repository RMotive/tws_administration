namespace Foundation.Contracts.Server.Interfaces;
public interface IExposure<TEstela> 
    : IServing<TEstela>, IExposure {

    public new Guid Tracer { set; }
    public new TEstela Estela { set; }
}

public interface IExposure { }
