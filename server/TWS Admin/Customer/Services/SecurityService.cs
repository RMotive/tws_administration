using Customer.Contracts.Services.Interfaces;
using Customer.Exceptions.Services.Security;
using Customer.Managers;
using Customer.Models;

using Foundation.Exceptions.Datasources;
using Foundation.Records.Datasources;

using TWS_Security.Entities;
using TWS_Security.Repositories;
using TWS_Security.Sets;

namespace Customer.Services;

public class SecurityService
    : ISecurityService {
    readonly SessionManager SessionsManager;

    readonly AccountsRepository AccountsRepository;

    public SecurityService(AccountsRepository accountsRepository) {
        SessionsManager = SessionManager.Int;
        AccountsRepository = accountsRepository;
    }

    public async Task<ForeignSessionModel> InitSession(AccountIdentityModel Identity) {
        CriticalOperationResults<AccountEntity, Account> SearchAccountResult;
        try {
            SearchAccountResult = await AccountsRepository
                .Read(I => I.User == Identity.Identity, Foundation.ReadingBehavior.First);
        } catch (XRecordUnfound<AccountsRepository> X) {
            throw new XUnfoundUser(X);
        }

        // --> The account was found
        AccountEntity Account = SearchAccountResult.Successes[0];
        if (!Account.Password.SequenceEqual(Identity.Password))
            throw new XWrongPassword(Account, Identity.Password);

        SessionModel SessionInited = SessionsManager.InitSession(Account);
        return SessionInited.GeneratePublicDerivation();
    }
}
