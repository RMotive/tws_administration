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
            (nameof(Vin), [Required, new UniqueValidator(), new LengthValidator(17, 17)]),
            (nameof(Plate), [Required, new UniqueValidator()]),
            (nameof(Manufacturer), [Required, new UniqueValidator()]),
            (nameof(Motor), [Required, new UniqueValidator(), new LengthValidator(15, 16)]),
            (nameof(Sct), [Required, new UniqueValidator(), new LengthValidator(19, 20)]),
            (nameof(Maintenance), [Required, new UniqueValidator(),]),
            (nameof(Situation), [Required, new LengthValidator(1, 20)]),
            (nameof(Insurance), [Required, new UniqueValidator()]),
        ];

        return Container;
    }
}
