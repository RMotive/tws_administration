using Foundation.Migrations.Quality.Bases;
using Foundation.Utils;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Depots;

/// <summary>
///     Qualifies the <see cref="SolutionsDepot"/>.
/// </summary>
public class Q_SolutionsDepost
    : BQ_MigrationDepot<Solution, SolutionsDepot, TWSSecuritySource> {
    public Q_SolutionsDepost()
        : base(nameof(Solution.Name)) {
    }

    protected override Solution MockFactory() {
        return new() {
            Sign = RandomUtils.String(5),
            Name = RandomUtils.String(15),
            Description = "Q_DescriptionToken"
        };
    }
}
