using System.Reflection;
using Foundation.Contracts.Exceptions;

namespace Foundation;

/// <summary>
///     A defines an exception to indicate a ValidateRule operation failure.
///     
///     This is thrown by the delegator method related to the interface to call
///     a simpler API communication between the rules and the object validated.
///     
///     This exception indicates that the ValidateRule method got wrong
///     during its execution, usually this method is invoked by the
///     BObject validator orchestration.
/// </summary>
public class XValidateRule
    : BException {
    /// <summary>
    ///     Constant message template to lead the exception when its thrown.
    /// </summary>
    const string MESSAGE = "Reflection validation rule excepted";
    /// <summary>
    ///     Reference to the property that throws the exception
    /// </summary>
    readonly PropertyInfo PropertyInfo;

    /// <summary>
    ///     Generates a new exception object.
    ///     
    ///     This exception indicates that the ValidateRule method got wrong
    ///     during its execution, usually this method is invoked by the
    ///     BObject validator orchestration.
    /// </summary>
    /// <param name="propertyInfo"></param>
    public XValidateRule(PropertyInfo propertyInfo) 
        : base($"{MESSAGE} on {propertyInfo.Name}:{propertyInfo.ReflectedType}") {
            PropertyInfo = propertyInfo;
    }

    public override Dictionary<string, dynamic> ToDisplay() {
        return new Dictionary<string, dynamic> {
            {"Property Name", PropertyInfo.Name},
            {"Reflectection Type", PropertyInfo.ReflectedType!},
        };
    }
}
