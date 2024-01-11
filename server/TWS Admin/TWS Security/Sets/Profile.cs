using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Entities;

namespace TWS_Security.Sets;

public partial class Profile
    : BSet<Profile, ProfileEntity> {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (String.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);

        return Container;
    }
    protected override ProfileEntity Generate()
    => new(this);

    public override bool EqualsEntity(ProfileEntity Entity) {
        if (Id != Entity.Pointer) return false;
        if (Name != Entity.Name) return false;
        if (Description != Entity.Description) return false;

        return true;
    }
}
