using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Entities;

namespace TWS_Security.Sets;

public partial class Solution
    : BSet<Solution, SolutionEntity> {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sign { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (Id <= 0)
            Container.Add(nameof(Id), IntegrityFailureReasons.LessOrEqualZero);
        if (String.IsNullOrEmpty(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);
        if (String.IsNullOrEmpty(Sign))
            Container.Add(nameof(Sign), IntegrityFailureReasons.NullOrEmptyValue);

        return Container;
    }
    protected override SolutionEntity Generate()
    => new(this);
    public override bool EqualsEntity(SolutionEntity Entity) {
        if (Id != Entity.Pointer) return false;
        if (Name != Entity.Name) return false;
        if (!Sign.Equals(Entity.Sign, StringComparison.CurrentCultureIgnoreCase)) return false;
        if (Description != Entity.Description) return false;

        return true;
    }

}
