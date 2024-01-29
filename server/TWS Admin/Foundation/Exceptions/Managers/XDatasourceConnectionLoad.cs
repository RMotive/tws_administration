using Foundation.Contracts.Exceptions;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Managers;
public class XDatasourceConnectionLoad
    : BException {
    new private const string Message = "Unable to load datasource project connection properties";

    private readonly ConnectionLoadFailureReasons Reason;
    private readonly string PropertiesFileName;
    private readonly Exception? IOCritical;

    public XDatasourceConnectionLoad(ConnectionLoadFailureReasons Reason, string PropertiesFileName, Exception? IOCritical = null)
        : base($"{Message} ({Reason})") {
        this.Reason = Reason;
        this.PropertiesFileName = PropertiesFileName;
        this.IOCritical = IOCritical;
    }

    public override Dictionary<string, dynamic> GenerateFailure() {
        return new() {
            {nameof(Reason), Reason},
            {nameof(PropertiesFileName), PropertiesFileName},
            {nameof(IOCritical), IOCritical!},
        };
    }
}
