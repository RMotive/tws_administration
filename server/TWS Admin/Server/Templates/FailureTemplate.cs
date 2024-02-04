using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Contracts.Server.Interfaces;

using Server.Templates.Exposures;

namespace Server.Templates;

public class FailureTemplate<TException>
    : ITemplate<TException, FailureExposure<IExceptionExposure>>
    where TException : IException<IExceptionExposure> {

    [Required]
    public Guid Tracer { get; set; }
    public TException Estela { get; set; }

    public FailureTemplate(TException Failure) { 
        Tracer = Guid.NewGuid();
        Estela = Failure;
    }

    public FailureExposure<IExceptionExposure> GenerateExposure() {
        return new FailureExposure<IExceptionExposure>() {
            Tracer = Tracer,
            Estela = Estela.GenerateExposure(),
        };
    }
}
