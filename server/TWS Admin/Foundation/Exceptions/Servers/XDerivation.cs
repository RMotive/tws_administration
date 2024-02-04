using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;

namespace Foundation.Exceptions.Servers;
public class XDerivation 
    : BException<XFDerivation> {
    const string MESSAGE = "Wrong refelction derivation";

    public XDerivation() 
        : base($"{MESSAGE} FROM[] to[]") {
    }

    protected override XFDerivation DesignFailure() {
        throw new NotImplementedException();
    }
}
