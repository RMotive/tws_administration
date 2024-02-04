using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;

using Reason = Foundation.Enumerators.Exceptions.ServerConfigurationFailureReasons;

namespace Foundation.Exceptions.Servers;
public class XServerConfiguration
    : BException<XFServerConfiguration> {

    const string MESSAGE = "Exception loading server context";

    private readonly Reason Reason;
    private readonly DateTime Timemark;

    public XServerConfiguration(Reason reason)
        : base($"{MESSAGE}") {
        Reason = reason;
        Timemark = DateTime.UtcNow;
    }

    protected override XFServerConfiguration DesignFailure() 
    => new() {
    };
}
