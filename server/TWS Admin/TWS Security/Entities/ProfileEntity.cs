using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using System.ComponentModel.DataAnnotations;

using TWS_Security.Sets;

namespace TWS_Security.Entities;

/// <summary>
///     Represents the entity object of a Profile stored in the datasource, is a mirroring validation safe interface
///     between business operations and datasource sets.
///     
///     A profile is used to store a collection of solution permits. This profiles can be referenced to users/employees/etc.
///     To determine and override their permissions level about the solution features.
/// </summary>
public class ProfileEntity
    : BDatasourceEntity<Profile, ProfileEntity>
{
    /// <summary>
    ///     Profile name, used to identify it into the solution.
    /// </summary>
    [Required]
    public string Name { get; set; }
    /// <summary>
    ///     Profile description, to communicate special comments about the profile.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Creates a new entity of a profile.
    ///     
    ///     A profile is used to store collections of permits and its own information.
    ///     This collection of permits are used to calculate users/employees permissions level.
    /// </summary>
    public ProfileEntity()
    {
        Name = string.Empty;
    }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
    {
        throw new NotImplementedException();
    }
    protected override Profile GenerateSet()
    {
        throw new NotImplementedException();
    }
    public override bool EvaluateSet(Profile Set)
    {
        throw new NotImplementedException();
    }
}
