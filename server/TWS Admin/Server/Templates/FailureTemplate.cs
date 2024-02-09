using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Contracts.Server.Interfaces;

using Server.Templates.Exposures;

namespace Server.Templates;

public class FailureTemplate<TException, TExposure>
    : ITemplate<TException, FailureExposure<TExposure>>
    where TException : IException<IExceptionExposure>
    where TExposure : IExceptionExposure {

    [Required]
    public Guid Tracer { get; set; }
    public TException Estela { get; set; }

    public FailureTemplate(TException Failure) {
        Tracer = Guid.NewGuid();
        Estela = Failure;
    }

    public FailureExposure<TExposure> GenerateExposure() {
        return new FailureExposure<TExposure>() {
            Tracer = Tracer,
            Estela = (TExposure)Estela.GenerateExposure(),
        };
    }
}
