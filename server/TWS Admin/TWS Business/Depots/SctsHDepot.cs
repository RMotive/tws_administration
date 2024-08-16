using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="SctsHDepot"/> datasource entity mirror.
/// </summary>
public class SctsHDepot
: BMigrationDepot<TWSBusinessSource, SctH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="SctsHDepot"/>.
    /// </summary>
    public SctsHDepot() : base(new(), null) {
    }
}
