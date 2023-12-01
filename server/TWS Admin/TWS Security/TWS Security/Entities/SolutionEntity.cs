using System.ComponentModel.DataAnnotations;

using TWS_Security.Contracts.Bases;
using TWS_Security.Contracts.Interfaces;
using TWS_Security.Exceptions.Entities;
using TWS_Security.Models;

namespace TWS_Security.Entities;

/// <summary>
///     Represents a solution as an entity.
///     Stores all the relevant information of a solution.
/// </summary>
public class SolutionEntity : BDatasourceEntity<Solution>
{
    /// <summary>
    ///     Simplified way to identiy the solution into the business context.
    /// </summary>
    [Required]
    public string Name { get; set; }
    /// <summary>
    ///     Internal Sign reference to intercept the setter
    ///     and automatically convert it into upper case.
    /// </summary>
    private string _Sign;
    /// <summary>
    ///     Simplification acronym for solution name, a easier
    ///     way to call the current solution.
    ///     
    ///     It will always be converted to Upper Case
    /// </summary>
    public string Sign 
    { 
        get => _Sign; 
        set => _Sign = value.ToUpper();
    }
    /// <summary>
    ///     Description about the solution and its target.
    /// </summary>
    public string? Description { get; set; }

    public SolutionEntity()
    {
        Name = "";
        _Sign = "";
    }
    /// <summary>
    ///     Compares if the current Solution entity is equal than 
    ///     the given datasource set
    /// </summary>
    /// <param name="Set"> Datasource set derived from the entity to compare </param>
    /// <returns>
    ///     Wheter the current entity is equal than the given datasource set.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public override bool CompareWithDatasourceSet(Solution Set)
    {
        if(Pointer != Set.Id) return false;
        if(Name != Set.Name) return false;
        if(Description != Set.Description) return false;
        if(Sign != Set.Sign.ToUpper()) return false;

        return true;
    }
    /// <summary>
    ///     Validates the entity to build a datasource valid set.
    /// </summary>
    /// <returns> Datasource set </returns>
    /// <exception cref="Exception"> When the solution name is empty </exception>
    public override Solution BuildDatasourceSet()
    {
        if(String.IsNullOrWhiteSpace(Name)) throw new XBuildDatasourceSet(this.GetType());

        return new Solution
        {
            Id = Pointer,
            Name = Name,
            Description = Description,
            Sign = Sign,
        };
    }

    public override void LoadFromDatasourceSet(Solution Set)
    {
        Pointer = Set.Id;
        Name = Set.Name;
        Sign = Set.Sign;
        Description = Set.Description;
    }

}
