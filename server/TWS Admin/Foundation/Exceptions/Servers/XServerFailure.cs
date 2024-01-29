using Foundation.Contracts.Exceptions;

namespace Foundation.Exceptions.Servers;
public class XServerFailure
    : BException {
    const string MESSAGE = "Critical server error unrecognized";

    public XServerFailure() 
        : base(MESSAGE) {
    }

    public override Dictionary<string, dynamic> ToDisplay() {
        throw new NotImplementedException();
    }
}
