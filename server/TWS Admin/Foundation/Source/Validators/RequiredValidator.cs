using System.Reflection;

using CSM_Foundation.Source.Exceptions;
using CSM_Foundation.Source.Interfaces;

namespace CSM_Foundation.Source.Validators;
/// <summary>
///     <list type="number">
///         <listheader> <term> Coding: </term> </listheader>
///         <item> Value cannot be empty, is required </item>
///     </list> 
/// </summary>
public class RequiredValidator
    : IValidator {
    /// <summary>
    ///     <list type="number">
    ///         <listheader> <term> Coding: </term> </listheader>
    ///         <item> Value cannot be empty, is required </item>
    ///     </list> 
    /// </summary>
    public RequiredValidator() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Property"></param>
    /// <param name="Value"></param>
    /// <exception cref="XIValidator_Evaluate"></exception>
    public void Evaluate(PropertyInfo Property, object? Value) {
        if (Value is not null) {
            return;
        }

        throw new XIValidator_Evaluate(this, Property, 1, "Value cannot be empty, is required");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public bool Satisfy(Type Type) {
        return true;
    }
}
