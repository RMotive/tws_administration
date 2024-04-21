using System.Net;

using Foundation.Server.Bases;

namespace Server.Quality.Middlewares.Resources.Exceptions;
public class XQ_Exception
    : BServerTransactionException<XQ_ExceptionSituation> {
    public XQ_Exception() 
        : base("quality exception mock", HttpStatusCode.BadRequest, null, null) {
    }
}

public enum XQ_ExceptionSituation {
    Quality
}