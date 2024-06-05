using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Business.Sets;

public partial class Sct
: BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
                (nameof(Type), [Required, new LengthValidator(6,6)]),
                (nameof(Number), [Required, new LengthValidator(25,25)]),
                (nameof(Configuration), [Required, new LengthValidator(6,10)]),

        ];

        return Container;
    }
}
