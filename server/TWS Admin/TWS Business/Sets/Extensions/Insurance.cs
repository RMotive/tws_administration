using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Business.Sets;

public partial class Insurance
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Policy), [new UniqueValidator(), new LengthValidator(1, 20),]),
            (nameof(Expiration), [Required]),
            (nameof(Country), [new LengthValidator(2, 3)]),
        ];

        return Container;
    }

}
