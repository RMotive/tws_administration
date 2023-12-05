using TWS_Security.Models;
using TWS_Security.TWS_Security.Entities;

using Xunit;

namespace Quality.Entities;
public class PermitEntityTest
{
    private readonly Permit setMock;
    private readonly PermitEntity entityMock;
    private readonly PermitEntity entityEmptyMock;

    public PermitEntityTest()
    {
        entityEmptyMock = new();
        
        string name = "testing name";
        string description = "testing description";
        uint solution = 0;

        entityMock = new PermitEntity
        {
            Name = name,
            Description = description,
            Solution = solution,
        };
        setMock = new Permit
        {
            Name = name,
            Description = description,
            Solution = (int)solution,
        };
    }

    [Fact]
    public void CompareWithDatasourceSetTest()
    {
        bool testFactSuccess = entityMock.CompareWithDatasourceSet(setMock);
        bool testFactFail = entityEmptyMock.CompareWithDatasourceSet(setMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFail);
    }

    [Fact]
    public void LoadFromDatasourceSetTest()
    {
        PermitEntity testEntity = new();
        testEntity.LoadFromDatasourceSet(setMock);

        Assert.Equal(entityMock, testEntity);
        Assert.NotEqual<PermitEntity>(testEntity, new());
        Assert.NotEqual<PermitEntity>(entityMock, new());
    }
    [Fact]
    public void BuildDatasourceSet()
    {
        Permit testSet = entityMock.BuildDatasourceSet();

        Assert.Equal<Permit>(setMock, testSet);
    }
}
