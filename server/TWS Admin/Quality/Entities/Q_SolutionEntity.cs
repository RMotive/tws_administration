using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Entities;
public class Q_SolutionEntity {
    private readonly Solution _setMock;
    private readonly SolutionEntity _entityMock;
    private readonly SolutionEntity _entityEmptyMock;

    public Q_SolutionEntity() {
        string name = "testing solution";
        string sign = "tsn";

        _setMock = new() {
            Id = 1,
            Name = name,
            Sign = sign,
            Description = null,
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", "", null);
    }


    [Fact]
    public void EvaluateSet() {
        bool testFactSuccess = _entityMock.EvaluateSet(_setMock);
        bool testFactFailure = _entityEmptyMock.EvaluateSet(_setMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFailure);
        Assert.NotEqual(_entityMock, _entityEmptyMock);
    }
    [Fact]
    public void BuildSet() {
        Solution testFact = _entityMock.BuildSet();

        Assert.Equal(testFact.Id, _entityMock.Pointer);
        Assert.Equal(testFact.Name, _entityMock.Name);
        Assert.True(testFact.Sign.Equals(_entityMock.Sign, StringComparison.CurrentCultureIgnoreCase));
        Assert.Equal(testFact.Description, _entityMock.Description);
    }
}
