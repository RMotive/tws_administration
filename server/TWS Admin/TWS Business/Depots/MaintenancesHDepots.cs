using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="MaintenancesHDepot"/> datasource entity mirror.
/// </summary>
public class MaintenancesHDepot
: BSourceDepot<TWSBusinessSource, MaintenanceH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="MaintenancesHDepot"/>.
    /// </summary>
    public MaintenancesHDepot() : base(new(), null) {
    }
}
