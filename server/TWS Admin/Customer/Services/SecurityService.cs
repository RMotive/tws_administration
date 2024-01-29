using Customer.Managers;
using Customer.Models;

using Foundation.Records.Datasources;

using TWS_Security.Entities;
using TWS_Security.Repositories;
using TWS_Security.Sets;

namespace Customer;

public class SecurityService
    : ISecurityService {
    readonly SessionManager SessionsManager;

    readonly AccountsRepository AccountsRepository;

    public SecurityService(AccountsRepository accountsRepository) {
        SessionsManager = SessionManager.Int;
        AccountsRepository = accountsRepository;
    }

    public async Task<ForeignSessionModel> InitSession(AccountIdentityModel Identity) {
        CriticalOperationResults<AccountEntity, Account> SearchAccountResult = await AccountsRepository
            .Read(I => I.User == Identity.Identity, Foundation.ReadingBehavior.First);
        if(SearchAccountResult.Failed > 0) 
            throw SearchAccountResult.Failures[0].Failure;

        // --> The account was found
        AccountEntity Account = SearchAccountResult.Successes[0];


        return new ForeignSessionModel("answer", [2, 3, 4]);
    }
}
