using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="YardLog"/> datasource entity mirror.
/// </summary>
public class YardLogsDepot : BSourceDepot<TWSBusinessSource, YardLog> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="YardLog"/>.
    /// </summary>
    public YardLogsDepot() : base(new(), null) {
    }
}
