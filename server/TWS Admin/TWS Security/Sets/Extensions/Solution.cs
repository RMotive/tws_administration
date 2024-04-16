using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Security.Sets;

public partial class Solution
    : BMigrationSet {

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
            ..Container,
            (nameof(Name), [Required, new UniqueValidator(), new LengthValidator(1, 40),]),
            (nameof(Sign), [Required, new UniqueValidator(), new LengthValidator(5, 5),]),
        ];

        return Container;
    }
}
