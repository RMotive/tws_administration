using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

namespace TWS_Security.Sets;
public partial class Account
    : BSourceSet {

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
