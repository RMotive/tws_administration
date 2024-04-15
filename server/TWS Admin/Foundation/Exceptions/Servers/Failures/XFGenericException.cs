using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Foundation.Exceptions.Servers.Failures;
public class XFGenericException
    : IGenericFailure {
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, object> Failure { get; set; } = [];
    public Situation Situation { get; set; } = default!;
}
