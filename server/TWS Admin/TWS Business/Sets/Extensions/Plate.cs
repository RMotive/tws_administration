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
            (nameof(Identifier), [new LengthValidator(8, 12)]),
            (nameof(State), [new LengthValidator(2, 3)]),
            (nameof(Country), [new LengthValidator(2, 3)]),
            (nameof(Expiration), [Required]),
            (nameof(Truck), [Required,new PointerValidator(true)]),

        ];

        return Container;
    }

    public virtual Truck? TruckNavigation { get; set; }

}
