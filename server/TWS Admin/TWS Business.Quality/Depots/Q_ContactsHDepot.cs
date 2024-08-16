using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="ContactsHBDepot"/>.
/// </summary>
public class Q_ContactsHDepot
    : BQ_MigrationDepot<ContactH, ContactsHBDepot, TWSBusinessSource> {
    public Q_ContactsHDepot()
        : base(nameof(ContactH.Entity)) {
    }

    protected override ContactH MockFactory() {

        return new() {
            Entity = 1,
            Status = 1,
            Sequence = 1,
            Timemark = DateTime.Now,

            Email = RandomUtils.String(10)
        };
    }
}
