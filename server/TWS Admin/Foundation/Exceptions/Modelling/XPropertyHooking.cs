using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Modelling.Failures;

namespace Foundation;

public class XPropertyHooking
    : BException<XFPropertyHooking> {
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

    protected override XFPropertyHooking DesignFailure()
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {nameof(PropertyName), PropertyName},
            {nameof(Reflection), Reflection},
        },
    };
}
