using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="AddressesDepot"/>.
/// </summary>
public class Q_AddressesDepot
    : BQ_MigrationDepot<Address, AddressesDepot, TWSBusinessSource> {
    public Q_AddressesDepot()
        : base(nameof(Address.Id)) {
    }

    protected override Address MockFactory() {

        return new() {
            Country = "USA"
        };
    }
}