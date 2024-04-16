using Foundation.Migrations.Bases;

using TWS_Security.Sets;

namespace TWS_Security.Depots;
public class SolutionsDepot
    : BMigrationDepot<TWSSecuritySource, Solution> {
    public SolutionsDepot() 
        : base(new()) {
    }
}
