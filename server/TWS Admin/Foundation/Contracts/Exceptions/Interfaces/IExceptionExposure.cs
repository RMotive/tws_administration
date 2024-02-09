namespace Foundation.Contracts.Exceptions.Interfaces;

public interface IExceptionExposure<TFailureExposition>
    : IExceptionExposure
    where TFailureExposition : new() {
    public TFailureExposition Failure { get; set; }
}

public interface IExceptionExposure {
    public string Message { get; set; }
}