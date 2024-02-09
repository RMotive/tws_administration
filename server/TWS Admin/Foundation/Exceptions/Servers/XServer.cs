using System.Diagnostics;

using Foundation.Contracts.Exceptions.Bases;

namespace Foundation.Exceptions.Servers;
public class XServer
    : BException {

    public readonly Exception Link;
    public readonly StackTrace Invoker;

    public XServer(Exception Link)
        : base("Critical server error unrecognized") {
        this.Link = Link;
        Invoker = new StackTrace(1);
    }
}
