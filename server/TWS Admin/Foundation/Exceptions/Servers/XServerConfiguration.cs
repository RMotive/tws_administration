using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;

using Reason = Foundation.Enumerators.Exceptions.ServerConfigurationFailureReasons;

namespace Foundation.Exceptions.Servers;
public class XServerConfiguration
    : BException<XFServerConfiguration> {

    const string MESSAGE = "Exception loading server context";

    private readonly Reason Reason;
    public XServerConfiguration(Reason reason)
        : base($"{MESSAGE}") {
        Reason = reason;
    }

    protected override XFServerConfiguration DesignFailure()
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {nameof(Reason), Reason },
        }
    };
    public override Dictionary<string, dynamic> GenerateAdvising()
    => new() {
        {nameof(Reason), Reason},
    };
}
