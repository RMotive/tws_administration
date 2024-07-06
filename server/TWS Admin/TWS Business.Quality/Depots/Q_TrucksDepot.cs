

using CSMFoundation.Migration.Quality.Bases;
using CSMFoundation.Utils;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="TruckDepot"/>.
/// </summary>
public class Q_TruckDepot
    : BQ_MigrationDepot<Truck, TruckDepot, TWSBusinessSource> {
    public Q_TruckDepot()
        : base(nameof(Truck.Vin)) {
    }

    protected override Truck MockFactory() {

        return new() {
            Vin = RandomUtils.String(17),
            Motor =RandomUtils.String(16),
            Manufacturer = 1,

        };
    }
}
