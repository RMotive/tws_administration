using Foundation.Exceptions.Datasources;

using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Entities;
public class Q_ProfileEntity
{
    private readonly Profile _setMock;
    private readonly ProfileEntity _entityMock;
    private readonly ProfileEntity _entityEmptyMock;

    public Q_ProfileEntity()
    {
        _setMock = new()
        {
            Id = 1,
            Name = "testing profile name",
            Description = "testing profile description",
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", null);
    }
    [Fact]
    public void BuildSet()
    {
        Profile testFact = _entityMock.BuildSet();

        Assert.Equal(testFact.Id, _entityMock.Pointer);
        Assert.Equal(testFact.Name, _entityMock.Name);
        Assert.Equal(testFact.Description, _entityMock.Description);
        Assert.Throws<XEntityIntegrity<Profile, ProfileEntity>>(new ProfileEntity("", null).BuildSet);
    }
    [Fact]
    public void EvaluateSet()
    {
        bool testFactSuccess = _entityMock.EvaluateSet(_setMock);
        bool testFactFailure = _entityEmptyMock.EvaluateSet(_setMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFailure);
        Assert.NotEqual(_entityMock, _entityEmptyMock);
    }
}
