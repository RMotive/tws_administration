using System.Text;
using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Unit.Sets;
public class Q_Account 
    : BQ_Set<Account, AccountEntity> {
    protected override (Account SetMock, AccountEntity EntityMock, AccountEntity XEntityMock) InitMocks() {
        string user = "Quality User";
        byte[] pass = Encoding.UTF8.GetBytes("QualityPassword2023");

        Account SetMock = new() {
            Id = 1,
            User = user,
            Password = pass,
        };
        AccountEntity EntityMock = new(SetMock);
        AccountEntity XEntityMock = new("", [], false);

        return (SetMock, EntityMock, XEntityMock);
    }

    public override void BuildSet() {
        AccountEntity testFact = SetMock.GenerateEntity();

        Assert.Equal(testFact.Pointer, SetMock.Id);
        Assert.Equal(testFact.User, SetMock.User);
        Assert.Equal(testFact.Password, SetMock.Password);
    }

    public override void EvaluateSet() {
        bool factSuccess = SetMock.EqualsEntity(EntityMock);
        bool factFailure = SetMock.EqualsEntity(XEntityMock);

        Assert.True(factSuccess);
        Assert.False(factFailure);
        Assert.NotEqual(EntityMock, XEntityMock);
    }
}
