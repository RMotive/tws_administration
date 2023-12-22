using Foundation.Exceptions.Datasources;

using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Sets;
public class Q_Permit
{
    private readonly Permit _setMock;
    private readonly PermitEntity _entityMock;
    private readonly PermitEntity _entityEmptyMock;
    public Q_Permit()
    {
        _setMock = new()
        {
            Id = 1,
            Name = "testing permit name",
            Description = "testing permit description",
            Solution = 1,
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", "", 0);
    }

    [Fact]
    public void EvaluateEntity()
    {
        bool testFactSuccess = _setMock.EvaluateEntity(_entityMock);
        bool testFactFailure = _setMock.EvaluateEntity(_entityEmptyMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFailure);
        Assert.NotEqual(_entityMock, _entityEmptyMock);
    }
    [Fact]
    public void BuildEntity()
    {
        PermitEntity testFact = _setMock.BuildEntity();

        Assert.Equal(testFact.Pointer, _setMock.Id);
        Assert.Equal(testFact.Name, _setMock.Name);
        Assert.Equal(testFact.Description, _setMock.Description);
        Assert.Equal(testFact.Solution, (uint)_setMock.Solution);
        Assert.Throws<XSetIntegrity<Permit, PermitEntity>>(new Permit().BuildEntity);
    }
}
