
using CSMFoundation.Migration.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots
{
    /// <summary>
    ///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
    ///     representing a depot to handle <see cref="Truck"/> datasource entity mirror.
    /// </summary>
    public class TruckDepot : BMigrationDepot<TWSBusinessSource, Truck>
    {
        /// <summary>
        ///     Generates a new depot handler for <see cref="Truck"/>.
        /// </summary>
        public TruckDepot()
            : base(new(), null)
        {
        }
    }


}
