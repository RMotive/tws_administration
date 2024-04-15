using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Modelling.Failures;

namespace Foundation;

public class XGetProperty
    : BException<XFPropertyHooking> {
    /// <summary>
    ///     Constant message template to lead the exception when its thrown.
    /// </summary>
    const string MESSAGE = "While trying to hook a reflection property";
    private readonly string PropertyName;
    private readonly Type Reflection;

    public XGetProperty(Type reflection, string name)
        : base($"{MESSAGE}({name}) on ({reflection})") {
        Reflection = reflection;
        PropertyName = name;
    }

    public override Dictionary<string, dynamic> GenerateAdvising()
    => new() {
        {nameof(PropertyName), PropertyName},
        {nameof(Reflection), Reflection},
    };
    protected override XFPropertyHooking DesignFailure()
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {nameof(PropertyName), PropertyName},
            {nameof(Reflection), Reflection},
        },
    };
}
