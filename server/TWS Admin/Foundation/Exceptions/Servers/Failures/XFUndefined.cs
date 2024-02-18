using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Foundation.Exceptions.Servers.Failures;
public class XFUndefined
    : IGenericFailure {
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, dynamic> Failure { get; set; } = [];
    public Situation Situation { get; set; } = default!;
}
