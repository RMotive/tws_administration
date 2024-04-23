namespace Foundation.Server.Records;
public record ServerExceptionPublish {
    required public string Trace { get; init; }
    required public int Sitaution { get; init; }
    required public string Advise { get; init; }
    required public string System {  get; init; }
    public Dictionary<string, dynamic> Factors { get; init; } = [];
}
