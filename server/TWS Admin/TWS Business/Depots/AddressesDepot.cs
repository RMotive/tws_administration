using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BMigrationDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Address"/> datasource entity mirror.
/// </summary>
public class AddressesDepot : BMigrationDepot<TWSBusinessSource, Address> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Address"/>.
    /// </summary>
    public AddressesDepot() : base(new(), null) {
    }
}
