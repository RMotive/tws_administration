using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="TrucksCommonsDepot"/>.
/// </summary>
public class Q_TrucksCommonsDepot
    : BQ_MigrationDepot<TruckCommon, TrucksCommonsDepot, TWSBusinessSource> {
    public Q_TrucksCommonsDepot()
        : base(nameof(TruckCommon.Id)) {
    }

    protected override TruckCommon MockFactory() {

        return new() {
            Vin = RandomUtils.String(17),
            Economic = RandomUtils.String(16),
            Carrier = 1
        };
    }
}