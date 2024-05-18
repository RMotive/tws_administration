using System.Text.Json.Serialization;

using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

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