using Foundation;
using Foundation.Contracts.Server.Interfaces;

namespace Server.Quality.Templates;
public class GenericExposure
    : ITemplateExposure<GenericExceptionExposure> {
    public Guid Tracer { get; set; }
    public GenericExceptionExposure Estela { get; set; } = default!;
}
