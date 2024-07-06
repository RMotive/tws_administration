using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Validators;
using System.Text.Json.Serialization;

namespace TWS_Business.Sets;

public partial class Manufacturer
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Model), [Required, new LengthValidator(1, 30)]),
            (nameof(Brand), [Required, new LengthValidator(1, 15)]),
            (nameof(Year), [Required]),
        ];

        return Container;
    }
}
