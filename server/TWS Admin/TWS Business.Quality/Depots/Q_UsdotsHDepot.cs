using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="UsdotHDepot"/>.
/// </summary>
public class Q_UsdotsHDepot
    : BQ_MigrationDepot<UsdotH, UsdotHDepot, TWSBusinessSource> {
    public Q_UsdotsHDepot()
        : base(nameof(UsdotH.Mc)) {
    }

    protected override UsdotH MockFactory() {

        return new() {
            Entity = 1,
            Status = 1,
            Sequence = 1,
            Timemark = DateTime.Now,

            Mc = RandomUtils.String(7),
            Scac = RandomUtils.String(4),
        };
    }
}
