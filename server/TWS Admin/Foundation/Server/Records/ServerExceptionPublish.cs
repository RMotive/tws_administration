namespace CSM_Foundation.Server.Records;
public record ServerExceptionPublish {
    public required string Trace { get; init; }
    public required int Situation { get; init; }
    public required string Advise { get; init; }
    public required string System { get; init; }
    public Dictionary<string, dynamic> Factors { get; init; } = [];
}
