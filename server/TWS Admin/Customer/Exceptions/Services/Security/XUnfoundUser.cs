using Customer.Exceptions.Services.Security.Failures;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Records.Exceptions;

using XRepository = Foundation.Exceptions.Datasources.XRecordUnfound<TWS_Security.Repositories.AccountsRepository>;

namespace Customer.Exceptions.Services.Security;
public class XUnfoundUser
    : BException<XFUnfoundUser> {
    const string MESSAGE = "The user account wasn't found in the live databases";
    const string DISPLAY = "Identity not found";

    readonly XRepository Exception;

    public XUnfoundUser(XRepository XRepository)
        : base(MESSAGE, new Situation(1, DISPLAY)) {
        Exception = XRepository;
    }

    protected override XFUnfoundUser DesignFailure()
    => new() {
        Message = MESSAGE,
        Situation = Situation,
        Failure = new() {
            {nameof(Exception.Message), Exception.Message },
            {$"{nameof(Exception.Mode)}:Code", Exception.Mode },
            {$"{nameof(Exception.Mode)}:Display", Exception.Mode.ToString() },
            {nameof(Exception.Method), Exception.Method },
            {nameof(Exception.Reference), Exception.Reference },
        },
    };
}
