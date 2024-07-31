using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="HPTruck"/> datasource entity mirror.
/// </summary>
public class HP_TruckDepot
: BMigrationDepot<TWSBusinessSource, HPTruck> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="HPTruck"/>.
    /// </summary>
    public HP_TruckDepot() : base(new(), null) {
    }
}
