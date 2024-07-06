using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Validators;
using System.ComponentModel.DataAnnotations;

namespace TWS_Security.Sets;
public partial class Contact
    : BMigrationSet {

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {

        RequiredValidator Required = new();

        Container = [
                ..Container,
                (nameof(Name), [Required, new LengthValidator(1,50)]),
                (nameof(Lastname), [Required, new LengthValidator(1,50)]),
                (nameof(Email), [Required, new UniqueValidator(),new LengthValidator(1,30)]),
                (nameof(Phone), [Required, new UniqueValidator(), new LengthValidator(10,14)]),

            ];

        return Container;
    }
}
