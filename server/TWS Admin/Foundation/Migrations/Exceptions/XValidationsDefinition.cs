namespace Foundation.Migrations.Exceptions;
public class XValidationsDefinition
    : Exception {
    public enum Reasons {
        Duplication,
        Unexist,
    }

    public IEnumerable<string> Properties;
    public Reasons Reason;
    public Type Origin;

    public XValidationsDefinition(IEnumerable<string> Properties, Reasons Reason, Type Origin)
        : base(
                $"{(Reason == Reasons.Duplication ? "Duplicated properties defined" : "Unexist properties to validate defined")} ({Origin})[{string.Join(',', Properties)}]"
            ) {
        this.Properties = Properties;
        this.Reason = Reason;
        this.Origin = Origin;
    }
}
