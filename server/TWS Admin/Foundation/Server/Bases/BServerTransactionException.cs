using System.Diagnostics;
using System.Net;

using Foundation.Server.Interfaces;
using Foundation.Server.Records;

namespace Foundation.Server.Bases;
public class BServerTransactionException<TSituation>
    : Exception, IServerTransactionException<TSituation>
    where TSituation : Enum {
    public string Trace { get; init; }
    public string Subject { get; protected set; } = string.Empty;
    public string Advise { get; protected set; } = string.Empty;
    public Exception? System { get; init; } = null;
    public TSituation Situation { get; protected set; } = default!;
    public Dictionary<string, dynamic> Details { get; init; } = [];
    public Dictionary<string, dynamic> Factors { get; init; } = [];
    public HttpStatusCode Status { get; init; }
    public BServerTransactionException(string Subject, HttpStatusCode Status, Exception? System = null)
        : base(System?.Message ?? Subject) {
        Trace = System?.StackTrace ?? new StackTrace().ToString();
        this.Details = Details ?? [];
        this.System = System;
        this.Subject = Subject;
        this.Status = Status;
    }

    public ServerExceptionPublish Publish() {
        return new ServerExceptionPublish() {
            Advise = Advise,
            Situation = Convert.ToInt32(Situation),
            System = (System?.GetType().ToString() ?? "N/A") + $"|{Message}",
            Trace = Trace[..200],
        };
    }
}
