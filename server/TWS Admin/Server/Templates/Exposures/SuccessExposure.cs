using Foundation.Contracts.Server.Interfaces;

namespace Server.Templates.Exposures;

public class SuccessExposure<TEstela> 
    : ITemplateExposure<TEstela> {
    public Guid Tracer { get; set; }
    public TEstela Estela { get; set; } = default!;

    public SuccessExposure() { }
}
