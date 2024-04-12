using System.Text;

using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Repositories;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Integration.Repositories;
public class Q_AccountsRepository
    : BQ_Repository<TWSSecuritySource, AccountsRepository, AccountEntity, Account> {
    public Q_AccountsRepository()
        : base(
            0,
            new(),
            new(),
            (token, set) => {
                set.User = token;
                return set;
            }
        ) { }

    protected override (Account, AccountEntity) MockFactory(int Pointer, string Token) {
        byte[] password = Encoding.UTF8.GetBytes(Token);

        return (
                new Account {
                    User = Token,
                    Password = password,
                    Wildcard = true
                },
                new AccountEntity(Token, password, true)
            );
    }
    protected override (Account, AccountEntity) XMockFactory() {
        return (
                new Account(),
                new AccountEntity("", [], false)
            );
    }
    protected override Account UMockFactory(int Pointer, string Token) {
        return new() {
            User = Token,
            Password = Encoding.UTF8.GetBytes(Token),
            Wildcard = true
        };
    }
}
