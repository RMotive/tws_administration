using System.Text;

using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Entities;
public class Q_AccountEntity {
    private readonly Account _setMock;
    private readonly AccountEntity _entityMock;
    private readonly AccountEntity _entityEmptyMock;

    public Q_AccountEntity() {
        string user = "Testing Account";
        byte[] password = Encoding.UTF8.GetBytes("testingAccount$21");

        _setMock = new() {
            Id = 1,
            User = user,
            Password = password
        };
        _entityMock = new(_setMock);
        _entityEmptyMock = new("", []);
    }

    [Fact]
    public void EvaluateSet() {
        bool testFactSuccess = _entityMock.EqualsSet(_setMock);
        bool testFactFailure = _entityEmptyMock.EqualsSet(_setMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFailure);
        Assert.NotEqual(_entityMock, _entityEmptyMock);
    }
    [Fact]
    public void BuildSet() {
        Account testFact = _entityMock.GenerateSet();

        Assert.Equal(testFact.Id, _entityMock.Pointer);
        Assert.Equal(testFact.User, _entityMock.User);
        Assert.Equal(testFact.Password, _entityMock.Password);
    }
}
