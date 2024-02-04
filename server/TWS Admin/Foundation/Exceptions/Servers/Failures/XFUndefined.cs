using Foundation.Contracts.Exceptions.Interfaces;

namespace Foundation.Exceptions.Servers.Failures;
public class XFUndefined
    : IGenericExceptionExposure {
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, dynamic> Failure { get; set; } = [];
}
