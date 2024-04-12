using System.Reflection;

namespace Foundation.Contracts.Records;
public interface IValidationRule {
    public PropertyInfo Property { get; }
    public Type SpecifiedType { get; }
    public string? ValidateRule(object? data);
}
