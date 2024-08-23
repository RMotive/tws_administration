using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="DriversCommonsDepot"/>.
/// </summary>
public class Q_DriversCommons
    : BQ_SourceDepot<DriverCommon, DriversCommonsDepot, TWSBusinessSource> {
    public Q_DriversCommons()
        : base(nameof(DriverCommon.Id)) {
    }

    protected override DriverCommon MockFactory() {

        return new() {
            License = RandomUtils.String(12),
            Situation = 1
        };
    }
}