namespace CSM_Foundation.Advisor.Interfaces;
public interface IAdvisingException {
    public string Subject { get; }
    public string Message { get; }
    public string Trace { get; }
    public Dictionary<string, dynamic> Details { get; }
}
