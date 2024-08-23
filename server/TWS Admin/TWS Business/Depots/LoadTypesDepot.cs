using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="LoadType"/> datasource entity mirror.
/// </summary>
public class LoadTypesDepot : BSourceDepot<TWSBusinessSource, LoadType> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="AxisLoadType/>.
    /// </summary>
    public LoadTypesDepot() : base(new(), null) {
    }
}
