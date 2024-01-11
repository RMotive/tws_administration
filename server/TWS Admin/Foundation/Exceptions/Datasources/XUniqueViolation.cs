using System.Reflection;
using Foundation.Contracts.Datasources.Interfaces;

using Foundation.Contracts.Exceptions;

namespace Foundation.Exceptions.Datasources;
public class XUniqueViolation<TSet>
    : BException
    where TSet : ISet {
    new const string Message = "Unique value constraint violation";

    private readonly Type Set;
    private readonly List<PropertyInfo> Violations;
    private readonly Exception? Reflex;
    public XUniqueViolation(List<PropertyInfo> Violations, Exception? Reflex = null)
        : base($"{Message} on {typeof(TSet)} with ({Violations.Count}) violations") {
        Set = typeof(TSet);
        this.Violations = Violations;
        this.Reflex = Reflex;
    }

    public override Dictionary<string, dynamic> ToDisplay() {
        return new() {
            {nameof(Set), Set},
            {nameof(Violations), Violations},
            {nameof(Reflex), Reflex?.Message ?? "---"},
        };
    }
}
