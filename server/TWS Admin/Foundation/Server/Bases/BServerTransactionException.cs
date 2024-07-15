using System.Diagnostics;
using System.Net;

using CSM_Foundation.Server.Interfaces;
using CSM_Foundation.Server.Records;

namespace CSM_Foundation.Server.Bases;
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
        Details ??= [];
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
