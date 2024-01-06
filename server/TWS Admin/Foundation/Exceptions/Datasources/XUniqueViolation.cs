using System.Reflection;

using Foundation.Contracts.Exceptions;

namespace Foundation.Exceptions.Datasources;
public class XUniqueViolation
    : BException {
    new const string Message = "Unique value constraint violation";

    private readonly Type SetType;
    private readonly List<PropertyInfo> Violations;

    public XUniqueViolation(Type SetType, List<PropertyInfo> Violations)
        : base($"{Message} on {SetType} with ({Violations.Count}) violations") {
        this.SetType = SetType;
        this.Violations = Violations;
    }

    public override Dictionary<string, dynamic> ToDisplay() {
        return new() {
            {nameof(SetType), SetType},
            {nameof(Violations), Violations},
        };
    }
}
