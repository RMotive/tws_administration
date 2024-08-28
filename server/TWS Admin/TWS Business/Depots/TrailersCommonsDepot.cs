using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TrailerCommon"/> datasource entity mirror.
/// </summary>
public class TrailersCommonsDepot : BSourceDepot<TWSBusinessSource, TrailerCommon> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TrailerCommon"/>.
    /// </summary>
    public TrailersCommonsDepot() : base(new(), null) {
    }
}
