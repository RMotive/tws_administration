using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using Microsoft.IdentityModel.Tokens;

using TWS_Security.Entities;

namespace TWS_Security.Sets;

public partial class Account
    : BDatasourceSet<Account, AccountEntity> {
    public int Id { get; set; }
    [Unique]
    public string User { get; set; } = null!;

    public byte[] Password { get; set; } = null!;
    protected override Dictionary<string, IntegrityFailureReasons> ValidateIntegrity(Dictionary<string, IntegrityFailureReasons> Container) {
        if (Id <= 0)
            Container.Add(nameof(Id), IntegrityFailureReasons.LessOrEqualZero);
        if (String.IsNullOrWhiteSpace(User))
            Container.Add(nameof(User), IntegrityFailureReasons.NullOrEmptyValue);
        if (Password.IsNullOrEmpty())
            Container.Add(nameof(Password), IntegrityFailureReasons.NullOrEmptyValue);

        return Container;
    }
    protected override AccountEntity GenerateEntity()
    => new(this);
    public override bool EvaluateEntity(AccountEntity Entity) {
        if (Id != Entity.Pointer) return false;
        if (User != Entity.User) return false;
        if (!Password.SequenceEqual(Entity.Password)) return false;

        return true;
    }
}