using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Trailer"/> datasource entity mirror.
/// </summary>
public class TrailersDepot : BMigrationDepot<TWSBusinessSource, Trailer> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Trailer"/>.
    /// </summary>
    public TrailersDepot() : base(new(), null) {
    }
}
