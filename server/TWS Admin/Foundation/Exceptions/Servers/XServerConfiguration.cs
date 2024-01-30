using Foundation.Contracts.Exceptions.Bases;

namespace Foundation.Exceptions.Servers;
public class XServerConfiguration
    : BException {
    public enum Reason {
        NotFound,
        Incomplete,
        WrongFormat,
    }

    private readonly Reason _reason;
    private readonly DateTime _timemark;

    public XServerConfiguration(Reason reason)
        : base("Exception loading server context") {
        this._reason = reason;
        _timemark = DateTime.UtcNow;
    }
}
