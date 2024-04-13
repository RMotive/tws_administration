using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Security.Sets;

public partial class Solution
    : BMigrationSet {

    protected override (string Property, IValidator<object?>[])[] Validations((string Property, IValidator<object?>[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Name), [Required, new UniqueValidator(), new LengthValidator(1, 40),]),
        ];

        return Container;
    }
}
