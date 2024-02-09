using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Contracts.Server.Interfaces;

namespace Server.Templates.Exposures;

public class FailureExposure<TEstela>
    : ITemplateExposure<TEstela>
    where TEstela : IExceptionExposure {
    public Guid Tracer { get; set; }
    public TEstela Estela { get; set; } = default!;

    public FailureExposure() {

    }
}
