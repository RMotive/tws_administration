using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers.Failures;

namespace Foundation.Exceptions.Servers;
public class XGenericException
    : BException<XFGenericException> {
    const string MESSAGE = "Generic exception has been thrown";

    private readonly BException Generic;


    public XGenericException(BException generic)
        : base(MESSAGE) {
        Generic = generic;
    }

    protected override XFGenericException DesignFailure()
    => new() {
        Message = MESSAGE,
        Failure = new() {
            {nameof(Generic.Message), Generic.Message},
            {nameof(Generic.Source), Generic?.Source ?? "" },
        },
    };
}
