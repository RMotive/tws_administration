namespace Foundation.Advising.Interfaces;
public interface IAdvisingException {
    public string Subject { get; }
    public string Message { get; }
    public string Trace { get; }
    public Dictionary<string, dynamic> Details { get; }
}
