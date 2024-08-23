using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Status"/> datasource entity mirror.
/// </summary>
public class StatusesDepot
: BSourceDepot<TWSBusinessSource, Status> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Status"/>.
    /// </summary>
    public StatusesDepot() : base(new(), null) {
    }
}
