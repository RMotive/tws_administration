using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

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
    : BEntity<Profile, ProfileEntity> {
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
    /// <param name="Name">
    ///     Profile identification name
    /// </param>
    /// <param name="Description">
    ///     Context related description.
    /// </param>
    public ProfileEntity(string Name, string? Description) {
        this.Name = Name;
        this.Description = Description;
    }
    /// <summary>
    ///     Creates a new entity of a profile.
    ///     
    ///     A profile is used to store collections of permits and its own information.
    ///     This collection of permits are used to calculate users/employees permissions level.
    /// </summary>
    /// <param name="Set">
    ///     Datasource Profile set representation, will be used to populate this 
    ///     entity object.
    /// </param>
    public ProfileEntity(Profile Set) {
        Pointer = Set.Id;
        Name = Set.Name;
        Description = Set.Description;
    }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (String.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);

        return Container;
    }
    protected override Profile Generate() {
        return new() {
            Id = Pointer,
            Name = Name,
            Description = Description,
        };
    }
    public override bool EqualsSet(Profile Set) {
        if (Pointer != Set.Id) return false;
        if (Name != Set.Name) return false;
        if (Description != Set.Description) return false;

        return true;
    }
}
