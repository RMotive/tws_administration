using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Exceptions;
using Foundation.Contracts.Exceptions.Bases;

using Server.Contracts.Interfaces;

namespace Server.Templates;

public class FailureTemplate<TFailure>
    : ITEmplate<TFailure>
    where TFailure : BException {

    [Required]
    public Guid Tracer { get; set; }
    public TFailure Estela { get; set; }

    public FailureTemplate(TFailure Failure) { 
        Tracer = Guid.NewGuid();
        Estela = Failure;
    }
}
