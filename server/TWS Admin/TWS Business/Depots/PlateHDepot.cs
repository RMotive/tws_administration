using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="PlatesHDepot"/> datasource entity mirror.
/// </summary>
public class PlatesHDepot
: BMigrationDepot<TWSBusinessSource, PlateH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="PlatesHDepot"/>.
    /// </summary>
    public PlatesHDepot() : base(new(), null) {
    }
}
