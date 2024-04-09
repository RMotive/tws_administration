using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Records.Exceptions;

namespace Foundation.Exceptions.Modelling.Failures;
public class XFRuleValidation
    : IGenericFailure {
    public Dictionary<string, object> Failure { get; set; } = [];
    public string Message { get; set; } = string.Empty;
    public Situation Situation { get; set; } = default!;
}
