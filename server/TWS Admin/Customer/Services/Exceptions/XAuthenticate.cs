using System.Net;

using CSMFoundation.Server.Bases;

namespace Customer.Services.Exceptions;
public class XAuthenticate
    : BServerTransactionException<XAuthenticateSituation> {
    public XAuthenticate(XAuthenticateSituation Situation)
        : base($"Authentication request has failed", HttpStatusCode.BadRequest, null) {

        this.Situation = Situation;
        this.Advise = Situation switch {
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