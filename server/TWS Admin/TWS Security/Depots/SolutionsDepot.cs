using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;

using TWS_Security.Sets;

namespace TWS_Security.Depots;

/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Solution"/> datasource entity mirror.
/// </summary>
public class SolutionsDepot
    : BSourceDepot<TWSSecuritySource, Solution> {
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
        : base(new(), null) {

    }
}
