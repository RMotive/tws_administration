namespace Foundation.Contracts.Exceptions.Interfaces;

public interface IExceptionExposure<TExceptionExposure> 
    : IExceptionExposure {
    public TExceptionExposure Failure {  get; set; }
}

public interface IExceptionExposure {
    public string Message { get; set; }
}