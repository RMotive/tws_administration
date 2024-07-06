using CSMFoundation.Migration.Interfaces;

using CSMFoundation.Migration.Bases;

using TWS_Security.Sets;

namespace TWS_Security.Depots;

/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Solution"/> datasource entity mirror.
/// </summary>
public class SolutionsDepot
    : BMigrationDepot<TWSSecuritySource, Solution> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Solution"/>.
    /// </summary>
    public SolutionsDepot(TWSSecuritySource Source, IMigrationDisposer? Disposer = null)
        : base(Source, Disposer) {
    }
    /// <summary>
    /// 
    /// </summary>
    public SolutionsDepot() 
        : base(new(), null){

    }
}
