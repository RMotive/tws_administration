using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;

namespace Foundation.Exceptions.Servers;
public class XDerivation 
    : BException<XFDerivation> {
    const string MESSAGE = "Wrong refelction derivation";

    private readonly Exception? Link;

    public XDerivation(Exception? link) 
        : base($"{MESSAGE} FROM[] to[]") {
        Link = link;
    }

    protected override XFDerivation DesignFailure()
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {"LinkMessage", Link?.Message ?? "Empty Link Message" }
        }
    };
}
