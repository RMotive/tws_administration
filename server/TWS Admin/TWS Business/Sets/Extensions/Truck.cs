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
            (nameof(Plate), [Required, new PointerValidator()]),
            (nameof(Manufacturer), [Required, new PointerValidator()]),
            (nameof(Motor), [new UniqueValidator(), new LengthValidator(15, 16)]),
            (nameof(Sct), [new UniqueValidator(), new LengthValidator(19, 20)]),
            (nameof(Maintenance), [Required, new PointerValidator()]),
            (nameof(Situation), [Required, new PointerValidator()]),
            (nameof(Insurance), [Required, new PointerValidator()]),
        ];

        return Container;
    }
}
