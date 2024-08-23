using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="UsdotH"/> datasource entity mirror.
/// </summary>
public class UsdotHDepot : BSourceDepot<TWSBusinessSource, UsdotH> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="UsdotH"/>.
    /// </summary>
    public UsdotHDepot() : base(new(), null) {
    }
}
