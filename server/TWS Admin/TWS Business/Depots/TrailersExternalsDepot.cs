using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TrailerExternal"/> datasource entity mirror.
/// </summary>
public class TrailersExternalsDepot : BSourceDepot<TWSBusinessSource, TrailerExternal> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TrailerExternal"/>.
    /// </summary>
    public TrailersExternalsDepot() : base(new(), null) {
    }
}
