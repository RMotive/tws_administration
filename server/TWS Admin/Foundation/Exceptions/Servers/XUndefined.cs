using System.Diagnostics;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;

namespace Foundation.Exceptions.Servers;
public class XUndefined
    : BException<XFUndefined> {
    const string MESSAGE = "Critical server error unrecognized";

    public readonly Exception? Link;
    public readonly StackTrace Invoker;

    public XUndefined(Exception Link) 
        : base(MESSAGE) {
        this.Link = Link;
        Invoker = new StackTrace(1);
    }

    protected override XFUndefined DesignFailure() 
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {"LinkMessage", Link?.Message ?? "Empty link message" }
        }
    };
}
