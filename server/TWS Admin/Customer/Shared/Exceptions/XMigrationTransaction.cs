using System.Net;

using Foundation.Migrations.Records;
using Foundation.Server.Bases;
using Foundation.Shared.Constants;

namespace Customer.Shared.Exceptions;
public class XMigrationTransaction
    : BServerTransactionException<XTransactionSituation> {
    public XMigrationTransaction(MigrationTransactionFailure[] Failures)
        : base($"Migration transaction has failed", HttpStatusCode.InternalServerError, null) {
        Situation = XTransactionSituation.Failed;
        Advise = AdvisesConstants.SERVER_CONTACT_ADVISE;

        Factors = Failures.ToDictionary<MigrationTransactionFailure, string, dynamic>(i => $"{i.Set.GetType()}({i.Set.Id})", i => i.System.Message);
        Details = Factors;
    }
}

public enum XTransactionSituation {
    Failed,
}
