using System.Reflection;

namespace Foundation;
public class SchemeIntegrityRule<TProperty> 
    : ISchemeIntegrityRule {
    public Type SpecifiedType {get; private set;}
    public PropertyInfo Property {get; private set;}
    public Func<TProperty, string?> Validator {get; private set;}

    public SchemeIntegrityRule(PropertyInfo property, Func<TProperty, string?> validator) {
        Property = property;
        Validator = validator;
        SpecifiedType = typeof(TProperty);
    }

    public string? ValidateRule(object? data) {        
        TProperty specifiedPropertyCast = (TProperty) data!;
        return Validator(specifiedPropertyCast!);
    }
}

public interface ISchemeIntegrityRule {
    public PropertyInfo Property {get;}
    public Type SpecifiedType {get;}
    public string? ValidateRule(object? data);
}