using TWS_Security.Contracts.Bases;
using TWS_Security.Exceptions.Entities;
using TWS_Security.Models;

namespace TWS_Security.TWS_Security.Entities;

/// <summary>
///     Represents a data entity of permits into the business
///     
///     A permit is a data bundle representation of a specific 
///     solution feature that can be used to allow or unallow 
///     users to perform them. 
/// </summary>
public class PermitEntity : BDatasourceEntity<Permit>
{
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
    /// </summary>
    public PermitEntity()
    {
        Name = String.Empty;
        Solution = 0;
    }

    /// <summary>
    ///     Compares if the current Entity is the same than the given Datasource Set
    /// </summary>
    /// <param name="Set">
    ///     Datasource Set Entity based.
    /// </param>
    /// <returns>
    ///     Wheter the Entity and the Datasource Set are equal.
    /// </returns>
    public override bool CompareWithDatasourceSet(Permit Set)
    {
        if(Pointer != Set.Id) return false;
        if(Name != Set.Name) return false;
        if(Description != Set.Description) return false;
        if(Solution != Set.Solution) return false;

        return true;
    }

    public override void LoadFromDatasourceSet(Permit Set)
    {
        Pointer = Set.Id;
        Name = Set.Name;
        Description = Set.Description;
        Solution = (uint)Set.Solution;
    }

    public override Permit BuildDatasourceSet()
    {
        XBuildDatasourceSet X = new(GetType());
        if(String.IsNullOrWhiteSpace(Name)) throw X;
        if(Solution < 0) throw X;

        return new Permit
        {
            Id = Pointer,
            Name = Name,
            Description = Description,
            Solution = (int)Solution,
        };
    }
}
