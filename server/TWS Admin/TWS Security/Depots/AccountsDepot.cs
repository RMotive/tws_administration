using Foundation.Migrations.Bases;

using TWS_Security.Sets;

namespace TWS_Security.Depots;
/// <summary>
///     Implements a new depot to handle <see cref="Account"/> entity
///     transactions. 
/// </summary>
public class AccountsDepot
    : BMigrationDepot<TWSSecuritySource, Account> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Account"/>.
    /// </summary>
    public AccountsDepot() 
        : base(new()) {

    }
}
