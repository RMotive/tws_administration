using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

namespace TWS_Security.Sets;
public partial class Contact
    : BSourceSet {

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
