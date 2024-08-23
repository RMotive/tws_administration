using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="TrailerClass"/> datasource entity mirror.
/// </summary>
public class TrailerClassesDepot : BMigrationDepot<TWSBusinessSource, TrailerClass> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="TrailerClass"/>.
    /// </summary>
    public TrailerClassesDepot() : base(new(), null) {
    }
}
