using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Customer.Exceptions.Services.Security.Failures;
public class XFWrongPassword
    : IGenericFailure {
    public Dictionary<string, object> Failure { get; set; } = [];
    public string Message { get; set; } = string.Empty;
    public Situation Situation { get; set; } = default!;
}
