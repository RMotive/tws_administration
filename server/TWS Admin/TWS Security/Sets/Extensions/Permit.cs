using System.Text.Json.Serialization;

using CSMFoundation.Migration.Bases;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Validators;

namespace TWS_Security.Sets;

public partial class Permit
    : BMigrationSet {

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