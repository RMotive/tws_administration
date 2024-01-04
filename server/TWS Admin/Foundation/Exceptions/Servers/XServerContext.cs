using Foundation.Contracts.Exceptions;

namespace Foundation.Exceptions.Servers;
public class XServerContext
    : BException {
    public enum Reason {
        NotFound,
        Incomplete,
        WrongFormat,
    }

    private readonly Reason _reason;
    private readonly DateTime _timemark;

    public XServerContext(Reason reason)
        : base("Exception loading server context") {
        this._reason = reason;
        _timemark = DateTime.UtcNow;
    }

    public override Dictionary<string, dynamic> ToDisplay() {
        return new Dictionary<string, dynamic>
        {
            {nameof(Reason), _reason},
            {nameof(DateTime), _timemark},
        };
    }
}
