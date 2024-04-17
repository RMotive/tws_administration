using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Business.Sets;

public partial class Manufacturer
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Model), [new LengthValidator(1, 30)]),
            (nameof(Brand), [new LengthValidator(1, 15)]),
            (nameof(Year), [Required]),
        ];

        return Container;
    }
}
