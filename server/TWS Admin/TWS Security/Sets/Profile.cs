using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Entities;

namespace TWS_Security.Sets;

public partial class Profile
    : BDatasourceSet<Profile, ProfileEntity>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
    {
        throw new NotImplementedException();
    }

    protected override ProfileEntity GenerateEntity()
    {
        throw new NotImplementedException();
    }

    public override bool EvaluateEntity(ProfileEntity Entity)
    {
        throw new NotImplementedException();
    }
}
