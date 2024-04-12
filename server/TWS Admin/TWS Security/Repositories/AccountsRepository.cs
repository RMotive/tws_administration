using Foundation.Contracts.Datasources.Bases;

using TWS_Security.Entities;
using TWS_Security.Sets;

namespace TWS_Security.Repositories;
public class AccountsRepository
    : BRepository<TWSSecuritySource, AccountsRepository, AccountEntity, Account> {

    public AccountsRepository() : base(new()) { }
}
