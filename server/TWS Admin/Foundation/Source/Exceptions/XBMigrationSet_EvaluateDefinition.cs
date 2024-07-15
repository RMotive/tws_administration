using CSM_Foundation.Source.Interfaces;

namespace CSM_Foundation.Source.Exceptions;
public class XBMigrationSet_EvaluateDefinition
    : Exception {
    public enum Reasons {
        Duplication,
        Unsatisfies,
        Unreflected,
        Unexist,
    }

    public IEnumerable<string> Properties;
    public IValidator? Validator;
    public Reasons Reason;
    public Type Origin;

    public XBMigrationSet_EvaluateDefinition(IEnumerable<string> Properties, Reasons Reason, Type Origin, IValidator? Validator)
        : base(
                $"{ReasonTitle(Reason)} ({Origin})[{string.Join(',', Properties)}] {Validator}"
            ) {
        this.Properties = Properties;
        this.Reason = Reason;
        this.Origin = Origin;
        this.Validator = Validator;
    }

    private static string ReasonTitle(Reasons Reason) {
        return Reason switch {
            Reasons.Duplication => $"Duplicated properties validations",
            Reasons.Unsatisfies => $"Property type unsatisfies validator type bounds",
            Reasons.Unreflected => $"Property to validate not contians reflected info",
            Reasons.Unexist => $"Properties to validate doesn't exist",
            _ => throw new NotImplementedException(),
        };
    }
}