using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Trailer"/> datasource entity mirror.
/// </summary>
public class TrailersDepot : BSourceDepot<TWSBusinessSource, Trailer> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Trailer"/>.
    /// </summary>
    public TrailersDepot() : base(new(), null) {
    }
}
