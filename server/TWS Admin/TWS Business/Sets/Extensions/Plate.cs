using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Business.Sets;

public partial class Plate
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Identifier), [Required, new LengthValidator(8, 12),]),
            (nameof(State), [Required, new LengthValidator(1, 30)]),
            (nameof(Country), [Required, new LengthValidator(1, 30)]),
            (nameof(Expiration), [Required]),

        ];

        return Container;
    }
}
