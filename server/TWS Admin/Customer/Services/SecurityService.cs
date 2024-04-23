using Customer.Managers;
using Customer.Managers.Records;
using Customer.Services.Exceptions;
using Customer.Services.Interfaces;
using Customer.Services.Records;
using Customer.Shared.Exceptions;

using Foundation.Migration.Enumerators;
using Foundation.Migrations.Records;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace Customer.Services;
public class SecurityService
    : ISecurityService {

    readonly AccountsDepot Accounts;
    readonly SessionsManager Sessions;
    public SecurityService(AccountsDepot Accounts) {
        this.Accounts = Accounts;
        Sessions = SessionsManager.Manager;
    }

    public async Task<Privileges> Authenticate(Credentials Credentials) {
        MigrationTransactionResult<Account> result = await Accounts.Read(i => i.User == Credentials.Identity, MigrationReadBehavior.First);
        if (result.Failed)
            throw new XMigrationTransaction(result.Failures);

        if (result.QTransactions == 0)
            throw new XAuthenticate(XAuthenticateSituation.Identity);

        Account account = result.Successes[0];
        if (!account.Password.SequenceEqual(Credentials.Password))
            throw new XAuthenticate(XAuthenticateSituation.Password);

        Session session = Sessions.Subscribe(Credentials, account.Wildcard);

        return new() {
            Token = session.Token,
            Expiration = session.Expiration,
            Identity = session.Identity,
            Wildcard = session.Wildcard,
        };
    }
}
