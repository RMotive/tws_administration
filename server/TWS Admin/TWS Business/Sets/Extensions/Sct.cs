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
                (nameof(Type), [new LengthValidator(6)]),
                (nameof(Number), [new LengthValidator(25)]),
                (nameof(Configuration), [new LengthValidator(10)]),

        ];

        return Container;
    }
}
