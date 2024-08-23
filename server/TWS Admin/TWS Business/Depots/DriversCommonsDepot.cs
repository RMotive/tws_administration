using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="DriverCommon"/> datasource entity mirror.
/// </summary>
public class DriversCommonsDepot : BMigrationDepot<TWSBusinessSource, DriverCommon> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="DriverCommon"/>.
    /// </summary>
    public DriversCommonsDepot() : base(new(), null) {
    }
}
