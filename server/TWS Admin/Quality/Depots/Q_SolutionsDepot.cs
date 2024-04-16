using Foundation.Managers;
using Foundation.Migrations.Quality.Bases;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Security.Quality.Depots;

/// <summary>
///     Qualifies the <see cref="SolutionsDepot"/>.
/// </summary>
public class Q_SolutionsDepost
    : BQ_MigrationDepot<Solution, SolutionsDepot, TWSSecuritySource> {

    protected override Solution MockFactory() {
        return new() {
            Sign = RandomManager.String(5),
            Name = RandomManager.String(15),
            Description = "Q_DescriptionToken"
        };
    }
}
