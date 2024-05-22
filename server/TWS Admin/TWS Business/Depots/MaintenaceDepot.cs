using Foundation.Migrations.Bases;
using TWS_Business.Sets;

namespace TWS_Business.Depots
{

    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Maintenance"/> datasource entity mirror.
    /// </summary>

    public class MaintenaceDepot
    : BMigrationDepot<TWSBusinessSource, Maintenance>
    {
        /// <summary>
        ///     Generates a new depot handler for <see cref="Maintenance"/>.
        /// </summary>
        public MaintenaceDepot() : base(new(), null)
        {
        }
    }
}
