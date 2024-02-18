using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Foundation.Exceptions.Servers.Failures;
public class XFServerConfiguration
    : IGenericFailure {
    public Dictionary<string, object> Failure { get; set; } = [];
    public string Message { get; set; } = string.Empty;
    public Situation Situation { get; set; } = default!;
}
