﻿using System.Reflection;

using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;

using Microsoft.IdentityModel.Tokens;

namespace Foundation.Migrations.Validators;
/// <summary>
///     <list type="number">
///         <listheader> <term> Coding: </term> </listheader>
///         <item> Value can't be null </item>
///         <item> Value doesn't reach min value </item>
///         <item> Value overrides max value </item>
///         <item> Unrecognized case </item>
///     </list> 
/// </summary>
public class LengthValidator
    : IValidator {
    /// <summary>
    /// 
    /// </summary>
    private readonly int? Min;
    /// <summary>
    /// 
    /// </summary>
    private readonly int? Max;
    /// <summary>
    ///     <list type="number">
    ///         <listheader> <term> Coding: </term> </listheader>
    ///         <item> Value can't be null </item>
    ///         <item> Value doesn't reach min value </item>
    ///         <item> Value overrides max value </item>
    ///         <item> Unrecognized case </item>
    ///     </list> 
    /// </summary>
    /// <param name="Min"></param>
    /// <param name="Max"></param>
    public LengthValidator(int? Min = null, int? Max = null) {
        this.Min = Min;
        this.Max = Max;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public bool Satisfy(Type Type) {
        if (Type == typeof(string)) return true;
        if (Type == typeof(IList<>)) return true;
        if (Type == typeof(IEnumerable<>)) return true;
        if (Type == typeof(ICollection<>)) return true;
        if (Type.IsArray) return true;

        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Property"></param>
    /// <param name="Value"></param>
    /// <exception cref="Exception"></exception>
    public void Evaluate(PropertyInfo Property, object? Value) {
        string message = "";
        int code = 0;
        if (Value is null) {
            message = "Value can't be null";
            code = 1;
        } else {
            switch (Value) {
                case string value:
                    if (Min != null && value.Length < Min) {
                        message = "Value doesn't reach min value";
                        code = 2;
                    } else if (Max != null && value.Length > Max) {
                        message = "Value overrides max value";
                        code = 3;
                    }
                    break;
                default:
                    message = "Unrecognized case";
                    code = 4;
                    break;
            };
        }

        if (message.IsNullOrEmpty()) return;
        throw new XIValidator_Evaluate(this, Property, code, message);
    }
}
