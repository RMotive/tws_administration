using System.Diagnostics;

using Foundation.Contracts.Exceptions;

namespace Foundation.Exceptions.Servers;
public class XServerFailure
    : BException {
    const string MESSAGE = "Critical server error unrecognized";

    public readonly Exception? Link;
    public readonly StackTrace? Invoker;

    public XServerFailure(Exception Link) 
        : base(MESSAGE) {
        this.Link = Link;
        Invoker = new StackTrace(1);
    }
    public XServerFailure() 
        : base(MESSAGE) {
        Invoker = new StackTrace(1);
    }

    public override Dictionary<string, dynamic> GenerateFailure() {
        throw new NotImplementedException();
    }
}
