using System.Net;

using CSM_Foundation.Server.Bases;

namespace Server.Quality.Suit.Middlewares.Resources.Exceptions;
public class XQ_Exception
    : BServerTransactionException<XQ_ExceptionSituation> {
    public XQ_Exception()
        : base("quality exception mock", HttpStatusCode.BadRequest, null) {
    }
}

public enum XQ_ExceptionSituation {
    Quality
}