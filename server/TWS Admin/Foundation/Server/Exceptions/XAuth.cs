using System.Net;

using CSM_Foundation.Core.Constants;
using CSM_Foundation.Server.Bases;

namespace CSM_Foundation.Server.Exceptions;
public class XAuth
    : BServerTransactionException<XAuthSituation> {
    public XAuth(XAuthSituation Situation)
        : base($"Unauthorized transaction request", HttpStatusCode.Unauthorized, null) {
        this.Situation = Situation;
        Advise = Situation switch {
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