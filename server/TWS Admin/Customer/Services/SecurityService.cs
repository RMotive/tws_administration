using CSM_Foundation.Source.Enumerators;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;

using TWS_Customer.Core.Exceptions;
using TWS_Customer.Managers;
using TWS_Customer.Managers.Records;
using TWS_Customer.Services.Exceptions;
using TWS_Customer.Services.Interfaces;
using TWS_Customer.Services.Records;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Customer.Services;
public class SecurityService
    : ISecurityService {
    private readonly AccountsDepot Accounts;
    private readonly SessionsManager Sessions;

    public SecurityService(AccountsDepot Accounts) {
        this.Accounts = Accounts;
        Sessions = SessionsManager.Manager;
    }

    public async Task<Session> Authenticate(Credentials Credentials) {
        static IQueryable<Account> include(IQueryable<Account> query) {
            return query
        .Include(c => c.ContactNavigation)
        .Select(a => new Account() {
            Id = a.Id,
            User = a.User,
            Password = a.Password,
            Wildcard = a.Wildcard,
            Contact = a.Contact,
            ContactNavigation = new Contact() {
                Id = a.ContactNavigation.Id,
                Name = a.ContactNavigation.Name,
                Lastname = a.ContactNavigation.Lastname,
                Email = a.ContactNavigation.Email,
                Phone = a.ContactNavigation.Phone
            },
        });
        }

        SourceTransactionOut<Account> result = await Accounts.Read(i => i.User == Credentials.Identity, MigrationReadBehavior.First, include);
        if (result.Failed) {
            throw new XMigrationTransaction(result.Failures);
        }

        if (result.QTransactions == 0) {
            throw new XAuthenticate(XAuthenticateSituation.Identity);
        }

        Account account = result.Successes[0];
        if (!account.Password.SequenceEqual(Credentials.Password)) {
            throw new XAuthenticate(XAuthenticateSituation.Password);
        }

        Permit[] permits = await Accounts.GetPermits(account.Id);
        Session session = Sessions.Subscribe(Credentials, account.Wildcard, permits, account.ContactNavigation);

        return session;
    }
}
