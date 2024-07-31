using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="HP_TruckDepot"/>.
/// </summary>
public class Q_HPTrucksDepot
    : BQ_MigrationDepot<HPTruck, HP_TruckDepot, TWSBusinessSource> {
    public Q_HPTrucksDepot()
        : base(nameof(HPTruck.Vin)) {
    }

    protected override HPTruck MockFactory() {

        return new() {
            Vin = RandomUtils.String(17),
            Motor = RandomUtils.String(16),
            Creation = DateTime.Now,
        };
    }
}
