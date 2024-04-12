using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Sets;

using IntegrityLacks = System.Collections.Generic.Dictionary<string, Foundation.Enumerators.Exceptions.IntegrityFailureReasons>;

namespace TWS_Security.Entities;


/// <summary>
///     Entity for [Feature].
///     
///     Defines the Entity concept mirror from a Set concept of [Feature].
///     
///     [Feature] concept: A Feature is the representation of an ability along solutions.
/// </summary>
public class FeatureEntity
    : BEntity<Feature, FeatureEntity> {
    #region Properties

    /// <summary>
    ///     [Feature] name identification.
    /// </summary>
    [Required, MaxLength(25)]
    public string Name { get; init; }
    /// <summary>
    ///     [Feature] summary description.
    /// </summary>
    public string? Description { get; init; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Generates a new <see cref="FeatureEntity"/>.
    ///     
    ///     Specifying all properties.
    /// </summary>
    /// <param name="Name">
    ///     [Feature] name identification.
    /// </param>
    /// <param name="Description">
    ///     [Feature] summary description.
    /// </param>
    public FeatureEntity(string Name, string? Description) { 
        this.Name = Name;
        this.Description = Description;
    }
    /// <summary>
    ///     Generates a new <see cref="FeatureEntity"/>,
    ///     
    ///     Based on the base <see cref="Feature"/>.
    /// </summary>
    /// <param name="Set">
    ///     Base Set concept to build the Entity.
    /// </param>
    public FeatureEntity(Feature Set) {
        Pointer = Set.Id;
        Name = Set.Name;
        Description = Set.Description;
    }

    #endregion

    #region Protected Methods

    protected override Feature Generate() {
        return new Feature {
            Id = Pointer,
            Name = Name,
            Description = Description,
        };
    }
    protected override IntegrityLacks ValidateIntegrity(IntegrityLacks Container) {
        if (string.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);
        return Container;
    }
    protected override PropertyInfo[] EqualityExceptions() {
        return [
                HookProperty(nameof(Pointer)),
        ];
    }

    #endregion

    #region Public Methods

    public override bool EqualsSet(Feature Set) {
        if(Pointer != Set.Id) return false;
        if(Name != Set.Name) return false;
        if(Description != Set.Description) return false;

        return true;
    }

    #endregion
}
