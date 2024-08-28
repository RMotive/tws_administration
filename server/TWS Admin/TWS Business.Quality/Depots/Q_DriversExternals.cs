using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="DriversExternalsDepot"/>.
/// </summary>
public class Q_DriversExternals
    : BQ_SourceDepot<DriverExternal, DriversExternalsDepot, TWSBusinessSource> {
    public Q_DriversExternals()
        : base(nameof(DriverExternal.Id)) {
    }

    protected override DriverExternal MockFactory() {

        return new() {
            Status = 1,
            Common = 1,
            Identification = 1
        };
    }
}