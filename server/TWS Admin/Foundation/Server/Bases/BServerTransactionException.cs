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
    public Exception? System { get; init; }
    public TSituation Situation { get; protected set; } = default!;
    public Dictionary<string, dynamic> Details { get; init; }
    public HttpStatusCode Status { get; init; }
    public BServerTransactionException(string Subject, HttpStatusCode Status, Exception? Exception, Dictionary<string, dynamic>? Details = null)
        : base(Exception?.Message ?? Subject) {
        Trace = Exception?.StackTrace ?? new StackTrace().ToString();
        this.Details = Details ?? [];
        this.System = Exception;
        this.Subject = Subject;
        this.Status = Status;
    }

    public ServerExceptionPublish Publish() {
        return new ServerExceptionPublish() {
            Advise = Advise,
            Sitaution = Convert.ToInt32(Situation),
            System = System?.GetType().ToString() ?? "N/A",
            Trace = Trace,
        };
    }
}
