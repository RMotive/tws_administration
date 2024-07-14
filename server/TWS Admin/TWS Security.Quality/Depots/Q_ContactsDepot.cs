
using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Depots;

/// <summary>
///     Qualifies the <see cref="ContactsDepot"/>.
/// </summary>
public class Q_ContactsDepot
    : BQ_MigrationDepot<Contact, ContactsDepot, TWSSecuritySource> {
    public Q_ContactsDepot()
        : base(nameof(Contact.Name)) {
    }

    protected override Contact MockFactory() {
        return new() {
            Name = RandomUtils.String(50),
            Lastname = RandomUtils.String(50),
            Email = RandomUtils.String(30),
            Phone = RandomUtils.String(14),
        };
    }
}
