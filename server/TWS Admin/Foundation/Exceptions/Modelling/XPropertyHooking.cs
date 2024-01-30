using Foundation.Contracts.Exceptions.Bases;

namespace Foundation;

public class XPropertyHooking
    : BException {
    /// <summary>
    ///     Constant message template to lead the exception when its thrown.
    /// </summary>
    const string MESSAGE = "While trying to hook a reflection property";
    private readonly string PropertyName;
    private readonly Type Reflection;

    public XPropertyHooking(Type reflection, string name) 
        : base($"{MESSAGE}({name}) on ({reflection})") {
            Reflection = reflection;
            PropertyName = name;
    }

}
