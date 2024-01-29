using Foundation.Contracts.Exceptions;

namespace Foundation;

public class XHookProperty
    : BException {
    /// <summary>
    ///     Constant message template to lead the exception when its thrown.
    /// </summary>
    const string MESSAGE = "While trying to hook a reflection property";
    private readonly string PropertyName;
    private readonly Type Reflection;

    public XHookProperty(Type reflection, string name) 
        : base($"{MESSAGE}({name}) on ({reflection})") {
            Reflection = reflection;
            PropertyName = name;
    }

    public override Dictionary<string, dynamic> GenerateFailure() {
        return new Dictionary<string, dynamic>{
            {nameof(Reflection), Reflection},
            {nameof(PropertyName), PropertyName},
        };
    }
}
