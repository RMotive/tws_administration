using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Server.Interfaces;

using Server.Templates.Exposures;

namespace Server.Templates;

public class SuccessTemplate<TSuccess> 
    : ITemplate<TSuccess, SuccessExposure<TSuccess>> {
    [Required]
    public Guid Tracer { get; init; }
    public TSuccess Estela { get; }

    public SuccessTemplate(TSuccess success) {
        Estela = success;
    }

    public SuccessExposure<TSuccess> GenerateExposure() {
        return new () {
            Tracer = Tracer,
            Estela = Estela,
        };
    }
}
