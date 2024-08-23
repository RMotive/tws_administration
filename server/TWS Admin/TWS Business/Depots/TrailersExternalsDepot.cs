using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TrailerExternal"/> datasource entity mirror.
/// </summary>
public class TrailersExternalsDepot : BMigrationDepot<TWSBusinessSource, TrailerExternal> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TrailerExternal"/>.
    /// </summary>
    public TrailersExternalsDepot() : base(new(), null) {
    }
}
