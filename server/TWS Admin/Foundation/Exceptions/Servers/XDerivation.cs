using Foundation.Contracts.Exceptions;
using Foundation.Contracts.Exceptions.Bases;

namespace Foundation.Exceptions.Servers;
public class XDerivation 
    : BException {
    const string MESSAGE = "Wrong refelction derivation";

    public XDerivation() 
        : base($"{MESSAGE} FROM[] to[]") {
    }
}
