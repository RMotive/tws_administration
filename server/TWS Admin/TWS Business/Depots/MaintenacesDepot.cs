using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Maintenance"/> datasource entity mirror.
/// </summary>
public class MaintenacesDepot
: BSourceDepot<TWSBusinessSource, Maintenance> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Maintenance"/>.
    /// </summary>
    public MaintenacesDepot() : base(new(), null) {
    }
}
