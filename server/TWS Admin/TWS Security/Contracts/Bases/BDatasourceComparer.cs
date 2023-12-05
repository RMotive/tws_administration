using System.Reflection;

namespace TWS_Security.Contracts.Bases;

/// <summary>
///     Represents an inheritance link between datasource objects
///     that need specific equality comparisson between their properties.
/// </summary>
public abstract class BDatasourceComparer
{
    public abstract override bool Equals(object? Comparer);
    public override int GetHashCode() => GetHashCode();
}
