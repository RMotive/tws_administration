using Foundation.Attributes.Datasources;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Enumerators.Exceptions;

using Microsoft.IdentityModel.Tokens;

using TWS_Security.Entities;

using IntegrityLacks = System.Collections.Generic.Dictionary<string, Foundation.Enumerators.Exceptions.IntegrityFailureReasons>;

namespace TWS_Security.Sets;

/// <summary>
///     Set for [Account].
///     
///     Defines the Set concept of [Account].
///     
///     [Account] concept: An account is the definition for a combination of solution login credentials, and
///     preserved information.
/// </summary>
public partial class Account
    : BSet<Account, AccountEntity> {
    public override int Id { get; set; }
    [Unique]
    public string User { get; set; } = string.Empty;
    public byte[] Password { get; set; } = [];
    public bool Wildcard { get; set; } = false;

    protected override IntegrityLacks ValidateIntegrity(IntegrityLacks Container) {
        if (string.IsNullOrWhiteSpace(User))
            Container.Add(nameof(User), IntegrityFailureReasons.NullOrEmptyValue);
        if (Password.IsNullOrEmpty())
            Container.Add(nameof(Password), IntegrityFailureReasons.NullOrEmptyValue);

        return Container;
    }
    protected override AccountEntity Generate()
    => new(this);
    public override bool EqualsEntity(AccountEntity Entity) {
        if (Id != Entity.Pointer)
            return false;
        if (User != Entity.User)
            return false;
        if (!Password.SequenceEqual(Entity.Password))
            return false;
        if (Wildcard != Entity.Wildcard)
            return false;

        return true;
    }
}