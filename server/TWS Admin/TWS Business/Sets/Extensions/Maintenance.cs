using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Validators;

namespace TWS_Business.Sets;

public partial class Maintenance
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Anual), [Required]),
            (nameof(Trimestral), [Required]),
        ];

        return Container;
    }
}
