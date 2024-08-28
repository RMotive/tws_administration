using CSM_Foundation.Source.Bases;

using TWS_Business.Sets;

namespace TWS_Business.Depots;
/// <summary>
///     Implements a <see cref="BSourceDepot{TMigrationSource, TMigrationSet}"/>
///     representing a depot to handle <see cref="Employee"/> datasource entity mirror.
/// </summary>
public class EmployeesDepot : BSourceDepot<TWSBusinessSource, Employee> {
    /// <summary>
    ///     Generates a new depot handler for <see cref="Employee"/>.
    /// </summary>
    public EmployeesDepot() : base(new(), null) {
    }
}
