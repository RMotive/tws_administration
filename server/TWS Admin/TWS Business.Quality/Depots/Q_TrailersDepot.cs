using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="TrailersDepot"/>.
/// </summary>
public class Q_TrailersDepot
    : BQ_MigrationDepot<Trailer, TrailersDepot, TWSBusinessSource> {
    public Q_TrailersDepot()
        : base(nameof(Trailer.Id)) {
    }

    protected override Trailer MockFactory() {

        return new() {
            Manufacturer = 1,
            Common = 1,
            Status = 1
        };
    }
}