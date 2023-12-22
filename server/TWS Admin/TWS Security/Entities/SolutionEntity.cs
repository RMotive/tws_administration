using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using System.ComponentModel.DataAnnotations;

using TWS_Security.Sets;

namespace TWS_Security.Entities;

/// <summary>
///     Represents a solution as an entity.
///     Stores all the relevant information of a solution.
/// </summary>
public class SolutionEntity
    : BDatasourceEntity<Solution, SolutionEntity>
{
    /// <summary>
    ///     Simplified way to identiy the solution into the business context.
    /// </summary>
    [Required]
    public string Name { get; private set; }
    /// <summary>
    ///     Simplification acronym for solution name, a easier
    ///     way to call the current solution.
    ///     
    ///     It will always be converted to Upper Case
    /// </summary>
    [Required]
    public string Sign
    {
        get => _Sign;
        private set => _Sign = value.ToUpper();
    }
    [Required]
    private string _Sign;
    /// <summary>
    ///     Description about the solution and its target.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    ///     Creates a new Solution Entity.
    ///     
    ///     This Entity represents the mirror of a Solution set into the database.
    ///     Is used to represent the available solutions currently generated in all
    ///     the business context, and represents a specific solution into the
    ///     business solution matrix.
    /// </summary>
    /// <param name="Name">
    ///     Specific solution name
    /// </param>
    /// <param name="Sign">
    ///     Specific solution sign. 
    ///     Take in mind, this will be converted to Uppercase automatically.
    /// </param>
    /// <param name="Description">
    ///     A brief description about the solution, its scope, functions and features, etc.
    /// </param>
    public SolutionEntity(string Name, string Sign, string? Description)
    {
        _Sign = String.Empty;
        this.Name = Name;
        this.Sign = Sign;
        this.Description = Description;
    }
    /// <summary>
    ///     Creates a new Solution Entity.
    ///     
    ///     This Entity represents the mirror of a Solution set into the database.
    ///     Is used to represent the available solutions currently generated in all
    ///     the business context, and represents a specific solution into the
    ///     business solution matrix.
    /// </summary>
    /// <param name="Set">
    ///     The Set representation of this entity, the data of the set will be
    ///     used to populate the entity object.
    /// </param>
    public SolutionEntity(Solution Set)
    {
        _Sign = String.Empty;
        this.Pointer = Set.Id;
        this.Name = Set.Name;
        this.Sign = Set.Sign;
        this.Description = Set.Description;
    }

    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container)
    {
        if (String.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);
        if (String.IsNullOrWhiteSpace(Sign))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);

        return Container;
    }
    protected override Solution GenerateSet()
    {
        return new()
        {
            Id = Pointer,
            Name = Name,
            Sign = Sign,
            Description = Description,
        };
    }
    public override bool EvaluateSet(Solution Set)
    {
        if(Pointer != Set.Id) return false;
        if(Name != Set.Name) return false;
        if(!Sign.Equals(Set.Sign, StringComparison.CurrentCultureIgnoreCase)) 
            return false;
        if(Description != Set.Description) return false;

        return true;
    }
}
