using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Validators;

namespace TWS_Security.Sets;

public partial class Solution
    : BMigrationSet {

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        Container = [
            ..Container,
            (nameof(Name), [new UniqueValidator(), new LengthValidator(1, 40)]),
            (nameof(Sign), [new UniqueValidator(), new LengthValidator(5, 5)]),
        ];

        return Container;
    }
}
