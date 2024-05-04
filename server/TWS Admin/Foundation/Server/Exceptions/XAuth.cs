using System.Net;

using Foundation.Server.Bases;
using Foundation.Shared.Constants;

namespace Foundation.Server.Exceptions;
public class XAuth
    : BServerTransactionException<XAuthSituation> {
    public XAuth(XAuthSituation Situation) 
        : base($"Unauthorized transaction request", HttpStatusCode.Unauthorized, null) {
        this.Situation = Situation;
        this.Advise = Situation switch {
            XAuthSituation.Lack => AdvisesConstants.SERVER_CONTACT_ADVISE,
            XAuthSituation.Format => $"Wrong token format {AdvisesConstants.SERVER_CONTACT_ADVISE}",
            _ => AdvisesConstants.SERVER_CONTACT_ADVISE,
        };
    }
}

public enum XAuthSituation {
    Lack,
    Format,
}