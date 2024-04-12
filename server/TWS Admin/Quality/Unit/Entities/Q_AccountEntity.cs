using System.Text;
using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Unit.Entities;
public class Q_AccountEntity 
    : BQ_Entity<Account, AccountEntity> {
    protected override (Account SetMock, AccountEntity EntityMock, AccountEntity XEntityMock) InitMocks() {
        string user = "Quality Accont";
        byte[] password = Encoding.UTF8.GetBytes("QualityPassword2023");

        Account SetMock = new() {
            Id = 1,
            User = user,
            Password = password
        };
        AccountEntity EntityMock = new(SetMock);
        AccountEntity XEntityMock = new("", [], false);

        return (SetMock, EntityMock, XEntityMock);
    }

    public override void BuildSet() {
        Account fact = EntityMock.GenerateSet();

        Assert.Equal(fact.Id, EntityMock.Pointer);
        Assert.Equal(fact.User, EntityMock.User);
        Assert.Equal(fact.Password, EntityMock.Password);
    }

    public override void EvaluateSet() {
        bool factSuccess = EntityMock.EqualsSet(SetMock);
        bool factFailure = XEntityMock.EqualsSet(SetMock);

        Assert.True(factSuccess);
        Assert.False(factFailure);
        Assert.NotEqual(EntityMock, XEntityMock);
    }
}
