using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Axis"/> datasource entity mirror.
/// </summary>
public class AxesDepot : BMigrationDepot<TWSBusinessSource, Axis> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Axis"/>.
    /// </summary>
    public AxesDepot() : base(new(), null) {
    }
}
