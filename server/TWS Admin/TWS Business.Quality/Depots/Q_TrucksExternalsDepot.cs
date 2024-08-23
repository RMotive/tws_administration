using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="TrucksExternalsDepot"/>.
/// </summary>
public class Q_TrucksExternalsDepot
    : BQ_MigrationDepot<TruckExternal, TrucksExternalsDepot, TWSBusinessSource> {
    public Q_TrucksExternalsDepot()
        : base(nameof(TruckExternal.Id)) {
    }

    protected override TruckExternal MockFactory() {

        return new() {
            Status = 1,
            Common = 1,
        };
    }
}