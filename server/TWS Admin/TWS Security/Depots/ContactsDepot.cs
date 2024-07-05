using CSMFoundation.Migration.Interfaces;

using Foundation.Migrations.Bases;

using TWS_Security.Sets;

namespace TWS_Security.Depots;

/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Contact"/> datasource entity mirror.
/// </summary>
public class ContactsDepot
     : BMigrationDepot<TWSSecuritySource, Contact> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Contact"/>.
    /// </summary>
    public ContactsDepot(IMigrationDisposer? Disposer = null) : base(new(), Disposer) { }

    public ContactsDepot()
        : base(new(), null) {

    }

}
