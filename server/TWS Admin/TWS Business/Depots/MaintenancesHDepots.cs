using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="MaintenancesHDepot"/> datasource entity mirror.
/// </summary>
public class MaintenancesHDepot
: BMigrationDepot<TWSBusinessSource, MaintenanceH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="MaintenancesHDepot"/>.
    /// </summary>
    public MaintenancesHDepot() : base(new(), null) {
    }
}
