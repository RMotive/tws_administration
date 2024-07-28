using System.Reflection;

using CSM_Foundation.Source.Exceptions;
using CSM_Foundation.Source.Interfaces;

namespace CSM_Foundation.Source.Validators;
/// <summary>
///     <list type="number">
///         <listheader> <term> Coding: </term> </listheader>
///         <item> <description> Pointer cannot be named different than 'Id'. </description> </item>
///         <item> Pointer must be integer indexer and not null. </item>
///         <item> Pointer cannot be less or equal zero. </item>
///     </list> 
/// </summary>
public class PointerValidator
    : IValidator {

    public bool isDependency;
    public bool isRequired;
    /// <summary>
    ///     <list type="number">
    ///         <listheader> <term> Coding: </term> </listheader>
    ///         <item> <description> Pointer cannot be named different than 'Id'. </description> </item>
    ///         <item> Pointer must be integer indexer and not null. </item>
    ///         <item> Pointer cannot be less or equal zero. </item>
    ///     </list> 
    /// </summary>
    public PointerValidator(bool isDependency = false, bool isRequired = true) {
        this.isDependency = isDependency;
        this.isRequired = isRequired;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Satisfy(Type Type) {
        return Type == typeof(int);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Property"></param>
    /// <param name="Value"></param>
    /// <exception cref="XIValidator_Evaluate"></exception>
    public void Evaluate(PropertyInfo Property, object? Value) {
        string message;
        int code;
        int? value = Value as int?;
        if (Property.Name != "Id" && !isDependency) {
            message = "Pointer cannot be named different than 'Id'";
            code = 1;
        } else if ((value == null && isRequired) || (Value != null && value == null && !isRequired)) {
            message = "Pointer must be integer indexer and not null";
            code = 2;
        } else if (value <= 0 && isRequired) {
            message = "Pointer cannot be less or equal zero";
            code = 3;
        } else {
            return;
        }

        throw new XIValidator_Evaluate(this, Property, code, message);
    }
}
