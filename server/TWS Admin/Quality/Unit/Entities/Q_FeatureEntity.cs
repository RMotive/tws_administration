using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Unit.Entities;
public class Q_FeatureEntity
    : BQ_Entity<Feature, FeatureEntity> {
    protected override (Feature SetMock, FeatureEntity EntityMock, FeatureEntity XEntityMock) InitMocks() {
        string name = "Quality Ability";
        string description = "Quality description summary";

        Feature SetMock = new() {
            Id = 1,
            Name = name,
            Description = description,
        };
        FeatureEntity EntityMock = new(SetMock);
        FeatureEntity XEntityMock = new("", null);

        return (SetMock, EntityMock, XEntityMock);
    }

    public override void BuildSet() {
        Feature fact = EntityMock.GenerateSet();

        Assert.Equal(EntityMock.Name, fact.Name);
        Assert.Equal(EntityMock.Pointer, fact.Id);
        Assert.Equal(EntityMock.Description, fact.Description);
    }

    public override void EvaluateSet() {
        bool factSuccess = EntityMock.EqualsSet(SetMock);
        bool factFailure = XEntityMock.EqualsSet(SetMock);

        Assert.True(factSuccess);
        Assert.False(factFailure);
        Assert.NotEqual(EntityMock, XEntityMock);
    }
}
