using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Location"/> datasource entity mirror.
/// </summary>
public class LocationsDepot : BSourceDepot<TWSBusinessSource, Location> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Location"/>.
    /// </summary>
    public LocationsDepot() : base(new(), null) {
    }
}
