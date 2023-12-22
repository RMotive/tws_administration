using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Entities;
public class Q_PermitEntity
{
    private readonly Permit _setMock;
    private readonly PermitEntity _entityMock;
    private readonly PermitEntity _entityEmptyMock;

    public Q_PermitEntity()
    {
        string name = "test permit";
        string description = "testing description";
        uint solution = 1;

        _setMock = new()
        {
            Id = 1,
            Name = name,
            Description = description,
            Solution = (int)solution,
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", "", 0);
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

    [Fact]
    public void BuildSet()
    {
        Permit testFactSet = _entityMock.BuildSet();
        PermitEntity eM = _entityMock;

        Assert.Equal(testFactSet.Id, eM.Pointer);
        Assert.Equal(testFactSet.Name, eM.Name);
        Assert.Equal(testFactSet.Description, eM.Description);
        Assert.Equal(testFactSet.Solution, (int)eM.Solution);
    }
}
