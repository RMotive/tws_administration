using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Section"/> datasource entity mirror.
/// </summary>
public class SectionsDepot : BMigrationDepot<TWSBusinessSource, Section> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Section"/>.
    /// </summary>
    public SectionsDepot() : base(new(), null) {
    }
}
