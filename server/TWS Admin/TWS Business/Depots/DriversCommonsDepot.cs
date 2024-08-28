using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="DriverCommon"/> datasource entity mirror.
/// </summary>
public class DriversCommonsDepot : BSourceDepot<TWSBusinessSource, DriverCommon> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="DriverCommon"/>.
    /// </summary>
    public DriversCommonsDepot() : base(new(), null) {
    }
}
