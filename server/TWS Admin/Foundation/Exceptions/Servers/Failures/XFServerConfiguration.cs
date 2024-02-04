using Foundation.Contracts.Exceptions.Interfaces;

using Reason = Foundation.Enumerators.Exceptions.ServerConfigurationFailureReasons;

namespace Foundation.Exceptions.Servers.Failures;
public class XFServerConfiguration 
    : IGenericExceptionExposure {
    public Dictionary<string, object> Failure { get; set; } = [];
    public string Message { get; set; } = string.Empty;
}
