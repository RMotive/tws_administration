using Customer.Exceptions.Services.Security.Failures;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Records.Exceptions;

namespace Customer.Exceptions.Services.Security;
public class XWrongPassword
    : BException<XFWrongPassword> {
    const string SUBJECT = "User found but passwords byte arrays don't match";
    const string DISPLAY = "Wrong passoword";

    readonly dynamic Account;
    readonly byte[] Missmatch;

    public XWrongPassword(dynamic account, byte[] missmatch)
        : base(SUBJECT, new Situation(2, DISPLAY)) {
        Account = account;
        Missmatch = missmatch;
    }

    protected override XFWrongPassword DesignFailure()
    => new() {
        Message = SUBJECT,
        Situation = Situation,
        Failure = new() {
            {nameof(Account.User), Account.User },
            {nameof(Account.Password), Account.Password },
            {nameof(Missmatch), Missmatch },
        },
    };
}
