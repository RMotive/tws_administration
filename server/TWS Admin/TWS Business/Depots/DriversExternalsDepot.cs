using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="DriverExternal"/> datasource entity mirror.
/// </summary>
public class DriversExternalsDepot : BSourceDepot<TWSBusinessSource, DriverExternal> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="DriverExternal"/>.
    /// </summary>
    public DriversExternalsDepot() : base(new(), null) {
    }
}
