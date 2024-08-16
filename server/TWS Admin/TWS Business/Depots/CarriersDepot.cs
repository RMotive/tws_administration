using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Carrier"/> datasource entity mirror.
/// </summary>
public class CarriersDepot : BMigrationDepot<TWSBusinessSource, Carrier> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Carrier"/>.
    /// </summary>
    public CarriersDepot() : base(new(), null) {
    }
}
