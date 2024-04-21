using System.Net;

using Foundation.Server.Bases;

namespace Foundation.Shared.Exceptions;
public class XSystem
    : BServerTransactionException<XSystemSituations> {
    public XSystem(Exception Exception, Dictionary<string, dynamic>? Details = null)
        : base("System exception caught on transaction operation", HttpStatusCode.InternalServerError, Exception, Details) {

        Situation = XSystemSituations.System;
        Advise = "Contact your service administrator";
    }
}

public enum XSystemSituations {
    System
}