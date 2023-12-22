using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Sets;
public class Q_Solution
{
    private readonly Solution _setMock;
    private readonly SolutionEntity _entityMock;
    private readonly SolutionEntity _entityEmptyMock;

    public Q_Solution()
    {
        string name = "solution test name";
        string sign = "stn";

        _setMock = new()
        {
            Id = 1,
            Name = name,
            Sign = sign,
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", "", null);
    }

    [Fact]
    public void BuildEntity()
    {
        SolutionEntity testFact = _setMock.BuildEntity();

        Assert.Equal(testFact.Pointer, _setMock.Id);
        Assert.Equal(testFact.Name, _setMock.Name);
        Assert.Equal(testFact.Sign, _setMock.Sign.ToUpper());
        Assert.Equal(testFact.Description, _setMock.Description);
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
}
