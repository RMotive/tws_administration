using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Foundation.Exceptions.Servers.Failures;
public class XFServer
    : IGenericFailure {
    public Dictionary<string, object> Failure { get; set; } = default!;
    public string Message { get; set; } = default!;
    public Situation Situation { get; set; } = default!;
}
