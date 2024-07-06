using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Validators;

namespace TWS_Security.Sets;
public partial class Account
    : BMigrationSet {

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        Container = [
                ..Container,
                (nameof(User), [ new UniqueValidator(), new RequiredValidator() ]),
                (nameof(Password), [ new RequiredValidator() ]),
                (nameof(Contact), [new PointerValidator(true)]),

            ];

        return Container;
    }
}
