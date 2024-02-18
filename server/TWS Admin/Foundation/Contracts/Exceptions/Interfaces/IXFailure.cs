using Foundation.Records.Exceptions;

namespace Foundation.Contracts.Exceptions.Interfaces;

public interface IXFailure<TFailureExposition>
    : IXFailure
    where TFailureExposition : new() {
    public TFailureExposition Failure { get; set; }
}

public interface IXFailure {
    public string Message { get; set; }
    public Situation Situation { get; set; }
}