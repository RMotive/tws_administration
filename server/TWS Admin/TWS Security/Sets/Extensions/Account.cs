using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Security.Sets;
public partial class Account
    : BMigrationSet {

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        Container = [
                ..Container,
                (nameof(User), [ new UniqueValidator(), new RequiredValidator() ]),
                (nameof(Password), [ new RequiredValidator() ]),
            ];

        return Container;
    }
}
