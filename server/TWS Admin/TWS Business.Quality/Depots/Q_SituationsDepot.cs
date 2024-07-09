

using CSMFoundation.Core.Utils;
using CSMFoundation.Migration.Quality.Bases;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="SituationsDepot"/>.
/// </summary>
public class Q_SituationsDepot
    : BQ_MigrationDepot<Situation, SituationsDepot, TWSBusinessSource> {
    public Q_SituationsDepot()
        : base(nameof(Situation.Name)) {
    }

    protected override Situation MockFactory() {

        return new() {
            Name = RandomUtils.String(25),
            Description = RandomUtils.String(100),
        };
    }
}
