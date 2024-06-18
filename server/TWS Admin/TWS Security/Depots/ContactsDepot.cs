using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    public ContactsDepot(Action<DbContext, IMigrationSet[]>? Disposition = null) : base(new(), Disposition) { }

    public ContactsDepot()
        : base(new(), null) { 
    
    }
    
}
