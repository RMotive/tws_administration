using Foundation.Contracts.Exceptions.Interfaces;

namespace Foundation.Exceptions.Servers.Failures;
public class XFServerConfiguration
    : IGenericExceptionExposure {
    public Dictionary<string, object> Failure { get; set; } = [];
    public string Message { get; set; } = string.Empty;
}
