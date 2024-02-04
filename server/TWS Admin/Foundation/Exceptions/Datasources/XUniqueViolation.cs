using System.Reflection;

using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions.Bases;

namespace Foundation.Exceptions.Datasources;
public class XUniqueViolation<TSet>
    : BException
    where TSet : ISet {
    new const string Message = "Unique value constraint violation";

    public readonly Type Set;
    public readonly List<PropertyInfo> Violations;
    public readonly Exception? Reflex;
    public XUniqueViolation(List<PropertyInfo> Violations, Exception? Reflex = null)
        : base($"{Message} on {typeof(TSet)} with ({Violations.Count}) violations") {
        Set = typeof(TSet);
        this.Violations = Violations;
        this.Reflex = Reflex;
    }
}
