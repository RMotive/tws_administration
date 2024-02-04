namespace Foundation.Contracts.Server.Interfaces;
public interface ITemplateExposure<TEstela> 
    : IServing<TEstela>, IExposure {

    public new Guid Tracer { get; set; }
    public new TEstela Estela { get; set; }
}

public interface IExposure { }
