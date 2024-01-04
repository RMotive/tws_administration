using Foundation.Contracts.Datasources.Bases;

using TWS_Security.Entities;
using TWS_Security.Sets;

namespace TWS_Security.Repositories;
public class AccountsRepository
    : BDatasourceRepository<AccountsRepository, AccountEntity, Account, TWSSecuritySource> {
    
    public AccountsRepository()
        : base(new()) { }
}
