using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Contracts.Server.Interfaces;

using Server.Templates.Exposures;

namespace Server.Templates;

public class FailureTemplate<TException, TExposure>
    : ITemplate<TException, FailureExposure<TExposure>>
    where TException : IException<IXFailure>
    where TExposure : IXFailure {

    [Required]
    public Guid Tracer { get; init; }
    public TException Estela { get; }

    public FailureTemplate(TException failure, Guid tracer) {
        Estela = failure;
        Tracer = tracer;
    }

    public FailureExposure<TExposure> GenerateExposure() {
        return new FailureExposure<TExposure>() {
            Tracer = Tracer,
            Estela = (TExposure)Estela.GenerateExposure(),
        };
    }
}
