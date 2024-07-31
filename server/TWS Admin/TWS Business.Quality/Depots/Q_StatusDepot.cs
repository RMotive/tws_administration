using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="StatusesDepot"/>.
/// </summary>
public class Q_StatusDepot
    : BQ_MigrationDepot<Status, StatusesDepot, TWSBusinessSource> {
    public Q_StatusDepot()
        : base(nameof(Status.Name)) {
    }

    protected override Status MockFactory() {

        return new() {
            Name = RandomUtils.String(25),
            Description = RandomUtils.String(100)
        };
    }
}
