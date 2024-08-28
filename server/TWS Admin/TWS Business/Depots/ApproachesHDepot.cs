using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="ApproachesH"/> datasource entity mirror.
/// </summary>
public class ApproachesHDepot : BSourceDepot<TWSBusinessSource, ApproachesH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="ApproachesH"/>.
    /// </summary>
    public ApproachesHDepot() : base(new(), null) {
    }
}
