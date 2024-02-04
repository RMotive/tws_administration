using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Contracts.Server.Interfaces;
using Foundation.Models;

namespace Server.Quality.Templates;
public class GenericExposure
    : ITemplateExposure<IGenericExceptionExposure> {
    public Guid Tracer { get; set; }
    public IGenericExceptionExposure Estela { get; set; } = default!;
}
