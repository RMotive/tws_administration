using TWS_Security.Entities;
using TWS_Security.Exceptions.Entities;
using TWS_Security.Models;

using Xunit;

namespace Quality.Entities;

public class SolutionEntityTest
{
    private readonly SolutionEntity entityEmptyMock;
    private readonly SolutionEntity entityMock;
    private readonly Solution setMock;

    public SolutionEntityTest()
    {
        string name = "testing solution";
        string sign = "testing sign";
        string description = "testing descrption";

        entityEmptyMock = new();
        entityMock = new SolutionEntity
        {
            Name = name,
            Sign = sign,
            Description = description,
        };
        setMock = new Solution
        {
            Name = name,
            Sign = sign,
            Description = description,
        };
    }

    [Fact]
    public void CompareWithDatasourceSetTest()
    {
        bool testFactSuccess = entityMock.CompareWithDatasourceSet(setMock);
        bool testFactFail = entityMock.CompareWithDatasourceSet(new());

        Assert.True(testFactSuccess);
        Assert.False(testFactFail);
    }

    [Fact]
    public void BuildDatasourceSetTest()
    {
        
        Solution setBuilded = entityMock.BuildDatasourceSet();
        bool testFactSuccess = entityMock.CompareWithDatasourceSet(setBuilded);

        Assert.True(testFactSuccess);
        Assert.Throws<XBuildDatasourceSet>(entityEmptyMock.BuildDatasourceSet);
    }

    [Fact]
    public void LoadFromDatasourceSet()
    {
        SolutionEntity entityLoaded = new();
        entityLoaded.LoadFromDatasourceSet(setMock);

        bool testFactSuccess = entityLoaded.CompareWithDatasourceSet(setMock);

        Assert.True(testFactSuccess);
    }
}
