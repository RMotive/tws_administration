using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="TrucksHDepot"/>.
/// </summary>
public class Q_TrucksHDepot
    : BQ_MigrationDepot<TruckH, TrucksHDepot, TWSBusinessSource> {
    public Q_TrucksHDepot()
        : base(nameof(TruckH.Vin)) {
    }

    protected override TruckH MockFactory() {

        return new() {
            Vin = RandomUtils.String(17),
            Economic = RandomUtils.String(16),
            Status = 1,
            Timemark = DateTime.Now,
            Sequence = 1,
            Manufacturer = 1,
            Entity = 1
        };
    }
}
