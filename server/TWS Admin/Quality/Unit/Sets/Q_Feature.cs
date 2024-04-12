using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Unit.Sets;
public class Q_Feature
    : BQ_Set<Feature, FeatureEntity> {
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
        FeatureEntity fact = SetMock.GenerateEntity();

        Assert.Equal(SetMock.Id, fact.Pointer);
        Assert.Equal(SetMock.Name, fact.Name);
        Assert.Equal(SetMock.Description, fact.Description);
    }

    public override void EvaluateSet() {
        bool factSuccess = SetMock.EqualsEntity(EntityMock);
        bool factFailure = SetMock.EqualsEntity(XEntityMock);

        Assert.True(factSuccess);
        Assert.False(factFailure);
        Assert.NotEqual(EntityMock, XEntityMock);
    }
}
