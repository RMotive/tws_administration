using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TruckCommon"/> datasource entity mirror.
/// </summary>
public class TrucksCommonsDepot : BSourceDepot<TWSBusinessSource, TruckCommon> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TruckCommon"/>.
    /// </summary>
    public TrucksCommonsDepot() : base(new(), null) {
    }
}
