using System.Net;
using CSMFoundation.Server.Bases;

namespace CSMFoundation.Core.Exceptions;
public class XSystem
    : BServerTransactionException<XSystemSituations> {
    public XSystem(Exception Exception)
        : base("SystemInternal exception caught on transaction operation", HttpStatusCode.InternalServerError, Exception) {

        Situation = XSystemSituations.System;
        Advise = "Contact your service administrator";
        Factors = new() {
            { "SystemInternal", Exception.Message }
        };
    }
}

public enum XSystemSituations {
    System
}