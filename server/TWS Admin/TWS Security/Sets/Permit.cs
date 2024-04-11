using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Entities;

namespace TWS_Security.Sets;

public partial class Permit
    : BSet<Permit, PermitEntity> {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Solution { get; set; }

    public virtual Solution SolutionNavigation { get; set; } = null!;

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (String.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmpty);
        if (Solution <= 0)
            Container.Add(nameof(Solution), IntegrityFailureReasons.DependencyPointer);

        return Container;
    }
    protected override PermitEntity Generate()
    => new(this);
    public override bool EqualsEntity(PermitEntity Entity) {
        if (Id != Entity.Pointer) return false;
        if (Name != Entity.Name) return false;
        if (Description != Entity.Description) return false;
        if (Solution != Entity.Solution) return false;

        return true;
    }
}
