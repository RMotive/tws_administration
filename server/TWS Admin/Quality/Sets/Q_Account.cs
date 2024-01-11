using System.Text;

using TWS_Security.Entities;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Sets;
public class Q_Account {
    private readonly Account _setMock;
    private readonly AccountEntity _entityMock;
    private readonly AccountEntity _emptyEntityMock;

    public Q_Account() {
        string user = "TestUser";
        byte[] pass = Encoding.UTF8.GetBytes("testing2023$");

        _setMock = new() {
            Id = 1,
            User = user,
            Password = pass,
        };
        _entityMock = new(_setMock);
        _emptyEntityMock = new("", []);
    }


    [Fact]
    public void BuildEntityTest() {
        AccountEntity testFact = _setMock.GenerateEntity();

        Assert.Equal(testFact.Pointer, _setMock.Id);
        Assert.Equal(testFact.User, _setMock.User);
        Assert.Equal(testFact.Password, _setMock.Password);
    }

    [Fact]
    public void EvaluateEntityTest() {
        bool testFactSuccess = _setMock.EqualsEntity(_entityMock);
        bool testFactFailure = _setMock.EqualsEntity(_emptyEntityMock);

        Assert.True(testFactSuccess);
        Assert.False(testFactFailure);
        Assert.NotEqual(_entityMock, _emptyEntityMock);
    }
}
