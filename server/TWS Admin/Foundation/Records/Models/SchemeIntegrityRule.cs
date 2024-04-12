using System.Reflection;

using Foundation.Contracts.Records;

namespace Foundation;
public class SchemeIntegrityRule<TProperty>
    : IValidationRule {
    public Type SpecifiedType { get; private set; }
    public PropertyInfo Property { get; private set; }
    public Func<TProperty, string?> Validator { get; private set; }

    public SchemeIntegrityRule(PropertyInfo property, Func<TProperty, string?> validator) {
        Property = property;
        Validator = validator;
        SpecifiedType = typeof(TProperty);
    }

    public string? ValidateRule(object? data) {
        TProperty specifiedPropertyCast = (TProperty)data!;
        return Validator(specifiedPropertyCast!);
    }
}