using Foundation.Contracts.Exceptions.Interfaces;

namespace Foundation.Exceptions.Servers.Failures;
public class XFDerivation 
    : IGenericExceptionExposure {
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, object> Failure { get; set; } = [];
}
