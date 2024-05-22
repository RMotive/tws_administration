using Foundation.Migrations.Bases;
using TWS_Business.Sets;

namespace TWS_Business.Depots
{
    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Insurance"/> datasource entity mirror.
    /// </summary>
    public class InsurancesDepot : BMigrationDepot<TWSBusinessSource, Insurance>
    {
        /// <summary>
        ///     Generates a new depot handler for <see cref="Insurance"/>.
        /// </summary>
        public InsurancesDepot() : base(new(), null)
        {
        }
    }
}
