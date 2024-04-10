using System.Diagnostics;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;
using Foundation.Records.Exceptions;

namespace Foundation.Exceptions.Servers;
public class XServer
    : BException<XFServer> {
    const string MESSAGE = "Critical server error unrecognized";

    public readonly Exception Link;

    public readonly StackTrace Invoker;

    public XServer(Exception Link)
        : base(MESSAGE) {
        this.Link = Link;
        Invoker = new StackTrace(1);
    }

    protected override XFServer DesignFailure() {
        return new() {
            Message = MESSAGE,
            Failure = new() {
                {nameof(Link), Link},
                {nameof(Invoker), Invoker},
            },
            Situation = Situation.Default,
        };
    }
}
