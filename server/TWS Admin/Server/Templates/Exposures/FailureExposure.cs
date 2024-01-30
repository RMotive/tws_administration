using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Contracts.Server.Interfaces;

namespace Server.Templates.Exposures;

public class FailureExposure<TEstela> 
    : IExposure<TEstela> 
    where TEstela : IFailure {
    public Guid Tracer { get; set; }
    public TEstela Estela { get; set; } = default!;

    public FailureExposure() {

    }
}
