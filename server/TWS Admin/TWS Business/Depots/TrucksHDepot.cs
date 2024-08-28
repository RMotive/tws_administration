using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TruckH"/> datasource entity mirror.
/// </summary>
public class TrucksHDepot
: BSourceDepot<TWSBusinessSource, TruckH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TruckH"/>.
    /// </summary>
    public TrucksHDepot() : base(new(), null) {
    }
}
