using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="ContactH"/> datasource entity mirror.
/// </summary>
public class ContactsHBDepot : BMigrationDepot<TWSBusinessSource, ContactH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="ContactH"/>.
    /// </summary>
    public ContactsHBDepot() : base(new(), null) {
    }
}
