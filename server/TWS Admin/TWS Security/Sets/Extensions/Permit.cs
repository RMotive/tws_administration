using System.Text.Json.Serialization;

using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

namespace TWS_Security.Sets;

public partial class Permit
    : BSourceSet {

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        Container = [
            .. Container,
            (nameof(Name), [new UniqueValidator(), new LengthValidator(1, 50)]),
        ];

        return Container;
    }

    [JsonIgnore]
    public virtual Solution? SolutionNavigation { get; set; }
}