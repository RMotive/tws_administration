using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Managers;
public class XDatasourceConfiguration
    : BException {
    new private const string Message = "Unable to load datasource project connection properties";

    private readonly ConnectionLoadFailureReasons Reason;
    private readonly string PropertiesFileName;
    private readonly Exception? IOCritical;

    public XDatasourceConfiguration(ConnectionLoadFailureReasons Reason, string PropertiesFileName, Exception? IOCritical = null)
        : base($"{Message} ({Reason})") {
        this.Reason = Reason;
        this.PropertiesFileName = PropertiesFileName;
        this.IOCritical = IOCritical;
    }
}
