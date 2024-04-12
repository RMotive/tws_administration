using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using TWS_Security.Entities;

using IntegrityLacks = System.Collections.Generic.Dictionary<string, Foundation.Enumerators.Exceptions.IntegrityFailureReasons>;

namespace TWS_Security.Sets;

/// <summary>
///     Set for [Feature].
///     
///     Defines the Set concept of [Feature].
///     
///     [Feature] concept: A Feature is the representation of an ability along solutions.
/// </summary>
public class Feature
    : BSet<Feature, FeatureEntity> {
    public override int Id { get; set; }
    [Unique]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    #region Public Methods

    public override bool EqualsEntity(FeatureEntity Entity) {
        if (Id != Entity.Pointer) return false;
        if (Name != Entity.Name) return false;
        if (Description != Entity.Description) return false;

        return true;
    }

    #endregion

    #region Protected Methods

    protected override FeatureEntity Generate()
    => new(this);

    protected override IntegrityLacks ValidateIntegrity(IntegrityLacks Container) {
        if (string.IsNullOrWhiteSpace(Name))
            Container.Add(nameof(Name), IntegrityFailureReasons.NullOrEmptyValue);
        return Container;
    }

    #endregion
}
