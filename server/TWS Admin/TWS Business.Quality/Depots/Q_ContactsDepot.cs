using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="ContactsBDepot"/>.
/// </summary>
public class Q_ContactsDepot
    : BQ_MigrationDepot<Contact, ContactsBDepot, TWSBusinessSource> {
    public Q_ContactsDepot()
        : base(nameof(Contact.Email)) {
    }

    protected override Contact MockFactory() {
        return new() {
            Status = 1,
            Email = "mail@test.com"
        };
    }
}