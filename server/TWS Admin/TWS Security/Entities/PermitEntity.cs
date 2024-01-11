using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Sets;

namespace TWS_Security.Entities;

/// <summary>
///     Represents a data entity of permits into the business
///     
///     A permit is a data bundle representation of a specific 
///     solution feature that can be used to allow or unallow 
///     users to perform them. 
/// </summary>
public class PermitEntity
    : BEntity<Permit, PermitEntity> {
    /// <summary>
    ///     Descriptive permit name identifier
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///     Description related to the permit feature.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    ///     The pointer to the related solution set into the datasource.
    /// </summary>
    public uint Solution { get; set; }

    /// <summary>
    ///     Creates a solution permit feature entity.
    ///     
    ///     A permit entity represents a user access to certain business solution features
    ///     also is related to an specific solution.
    /// </summary>
    /// <param name="Name">
    ///     Permit name identification.
    /// </param>
    /// <param name="Description">
    ///     Permit description about it features.
    /// </param>
    /// <param name="Solution">
    ///     The solution where this permit effects.
    /// </param>
    public PermitEntity(string Name, string? Description, uint Solution) {
        this.Name = Name;
        this.Description = Description;
        this.Solution = Solution;
    }
    /// <summary>
    ///     Creates a solution permit feature entity.
    ///     
    ///     A permit entity represents a user access to certain business solution features
    ///     also is related to an specific solution.
    /// </summary>
    /// <param name="Set">
    ///     Permit set database representation, will be used to populate this
    ///     entity object data.
    /// </param>
    public PermitEntity(Permit Set) {
        this.Pointer = Set.Id;
        this.Name = Set.Name;
        this.Description = Set.Description;
        this.Solution = (uint)Set.Solution;
    }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (String.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);
        if (Solution <= 0)
            Container.Add(nameof(Solution), IntegrityFailureReasons.requiredValidDependencyPointer);

        return Container;
    }
    protected override Permit Generate() {
        return new Permit {
            Id = Pointer,
            Name = Name,
            Description = Description,
            Solution = (int)Solution,
        };
    }
    public override bool EqualsSet(Permit Set) {
        if (Pointer != Set.Id) return false;
        if (Name != Set.Name) return false;
        if (Description != Set.Description) return false;
        if (Solution != (uint)Set.Solution) return false;

        return true;
    }
}
