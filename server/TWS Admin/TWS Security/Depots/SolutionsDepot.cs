using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;

using Microsoft.EntityFrameworkCore;

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
    public SolutionsDepot(Action<DbContext, IMigrationSet[]>? Disposition = null)
        : base(new(), Disposition) {
    }

    public SolutionsDepot()
        : base(new(), null) {

    }
}
