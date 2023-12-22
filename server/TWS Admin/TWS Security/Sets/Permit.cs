using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Entities;

namespace TWS_Security.Sets;

public partial class Permit
    : BDatasourceSet<Permit, PermitEntity>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Solution { get; set; }

    public virtual Solution SolutionNavigation { get; set; } = null!;

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
    {
        if(String.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);
        if(Solution <= 0)
            Container.Add(nameof(Solution), IntegrityFailureReasons.requiredValidDependencyPointer);

        return Container;
    }
    protected override PermitEntity GenerateEntity()
    => new(this);
    public override bool EvaluateEntity(PermitEntity Entity)
    {
        if(Id != Entity.Pointer) return false;
        if(Name != Entity.Name) return false;
        if(Description != Entity.Description) return false;
        if(Solution != Entity.Solution) return false;

        return true;
    }
}
