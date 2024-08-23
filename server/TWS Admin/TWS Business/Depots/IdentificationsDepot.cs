using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Identification"/> datasource entity mirror.
/// </summary>
public class IdentificationsDepot : BMigrationDepot<TWSBusinessSource, Identification> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Axis"/>.
    /// </summary>
    public IdentificationsDepot() : base(new(), null) {
    }
}
