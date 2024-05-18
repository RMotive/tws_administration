using System.Net;

using Foundation.Advising.Interfaces;
using Foundation.Server.Records;

namespace Foundation.Server.Interfaces;
/// <summary>
///     Defines the behavior for an exception
///     thrown on server transaction time, this means
///     an exceptio thrown when a server request was being tried to
///     be resolved but there arised an exception.
/// </summary>
public interface IServerTransactionException<TSituation>
    : IAdvisingException, IServerTransactionException
    where TSituation : Enum {
    /// <summary>
    ///     Enumeration of possible situations for this exception.
    ///     <br> This field is used to handle validations along different sitautions codes easily </br>
    /// </summary>
    public TSituation Situation { get; }
}

public interface IServerTransactionException {
    /// <summary>
    ///     An user friendly message, usually used when the requester gets the transaction resolution.
    /// </summary>
    public string Advise { get; }
    /// <summary>
    ///     The System exception thrown that arised this server known exception.
    /// </summary>
    public Exception? System { get; }
    /// <summary>
    ///     Indicates a custom status code for the transaction resolution.
    /// </summary>
    public HttpStatusCode Status { get; }
    /// <summary>
    ///     Stores custom factors to analyze the thrown exception.
    /// </summary>
    public Dictionary<string, dynamic> Factors { get; }

    public ServerExceptionPublish Publish();
}