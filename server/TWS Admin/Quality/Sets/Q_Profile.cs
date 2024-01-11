using Foundation.Exceptions.Datasources;

using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Sets;
public class Q_Profile {
    private readonly Profile _setMock;
    private readonly ProfileEntity _entityMock;
    private readonly ProfileEntity _entityEmptyMock;

    public Q_Profile() {
        _setMock = new() {
            Id = 1,
            Name = "testing profile name",
            Description = "testing profile description",
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", null);
    }

    [Fact]
    public void BuildEntity() {
        ProfileEntity testFact = _setMock.GenerateEntity();

        Assert.Equal(testFact.Pointer, _setMock.Id);
        Assert.Equal(testFact.Name, _setMock.Name);
        Assert.Equal(testFact.Description, _setMock.Description);
        Assert.Throws<XSetIntegrity<Profile, ProfileEntity>>(new Profile().GenerateEntity);
    }
    [Fact]
    public void EvaluateEntity() {
        bool testFactSuccess = _setMock.EqualsEntity(_entityMock);
        bool testFactFailure = _setMock.EqualsEntity(_entityEmptyMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFailure);
        Assert.NotEqual(_entityMock, _entityEmptyMock);
    }
}
