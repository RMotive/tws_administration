using Foundation.Contracts.Exceptions.Bases;



// --> Aliases usage
using Failures = Foundation.Enumerators.Exceptions.DSReadingConfigurationsFailureReasons;

namespace Foundation.Exceptions.Managers;
public class XDSReadingConfigurations
    : BException {
    new private const string Message = "Unable to load datasource project connection properties";

    public readonly Failures Reason;
    public readonly string PropertiesFileName;
    public readonly Exception? IOCritical;

    public XDSReadingConfigurations(Failures Reason, string PropertiesFileName, Exception? IOCritical = null)
        : base($"{Message} ({Reason})") {
        this.Reason = Reason;
        this.PropertiesFileName = PropertiesFileName;
        this.IOCritical = IOCritical;
    }

    public override Dictionary<string, dynamic> GenerateAdvising()
    => new() {
        {nameof(Reason), Reason.ToString()},
        {nameof(PropertiesFileName), PropertiesFileName},
        {nameof(IOCritical), IOCritical?.Message ?? "None"}
    };
}
