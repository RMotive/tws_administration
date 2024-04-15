using System.Reflection;

using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Validators;
/// <summary>
/// 
/// </summary>
public class RequiredValidator
    : IValidator {
    /// <summary>
    /// 
    /// </summary>
    public RequiredValidator() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Property"></param>
    /// <param name="Value"></param>
    /// <exception cref="XIValidator_Evaluate"></exception>
    public void Evaluate(PropertyInfo Property, object? Value) {
        if (Value is not null) return;

        throw new XIValidator_Evaluate(this, Property, 1, $"Cannot be empty");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public bool Satisfy(Type Type) => true;
}
