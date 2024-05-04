using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;

using TWS_Security.Sets;

namespace TWS_Security.Quality.Sets;
public class Q_Account
    : BQ_MigrationSet<Account> {
    protected override Q_MigrationSet_EvaluateRecord<Account>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Account>[] Container) {
        Q_MigrationSet_EvaluateRecord<Account> success = new() {
            Mock = new() {
                Id = 1,
                User = "Q_User",
                Wildcard = true,
                Password = [1, 2, 3],
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Account> failure = new() {
            Mock = new(),
            Expectations = [
                (nameof(Account.Id), [(new PointerValidator(), 3)]),
                (nameof(Account.User), [(new RequiredValidator(), 1)]),
                (nameof(Account.Password), [(new RequiredValidator(), 1)]),
            ],
        };

        return [.. Container, success, failure];
    }
}
