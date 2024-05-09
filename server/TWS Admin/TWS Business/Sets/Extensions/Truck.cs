using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Business.Sets;

public partial class Truck
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Vin), [new UniqueValidator(), new LengthValidator(17, 17)]),
            (nameof(Manufacturer), [Required, new PointerValidator()]),
            (nameof(Motor), [new UniqueValidator(), new LengthValidator(15, 16)]),
            (nameof(Sct), [new PointerValidator()]),
            (nameof(Maintenance), [new PointerValidator()]),
            (nameof(Situation), [new PointerValidator()]),
            (nameof(Insurance), [new PointerValidator()]),
        ];

        return Container;
    }
}
