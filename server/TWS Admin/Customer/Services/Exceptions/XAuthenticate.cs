using System.Net;

using CSM_Foundation.Server.Bases;

namespace TWS_Customer.Services.Exceptions;
public class XAuthenticate
    : BServerTransactionException<XAuthenticateSituation> {
    public XAuthenticate(XAuthenticateSituation Situation)
        : base($"Authentication request has failed", HttpStatusCode.BadRequest, null) {

        this.Situation = Situation;
        Advise = Situation switch {
            XAuthenticateSituation.Identity => $"Identity not found",
            XAuthenticateSituation.Password => $"Wrong password",
            _ => throw new NotImplementedException(),
        };
    }
}

public enum XAuthenticateSituation {
    Identity,
    Password,
}