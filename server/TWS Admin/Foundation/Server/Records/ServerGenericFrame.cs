using CSMFoundation.Servers.Interfaces;

namespace CSMFoundation.Server.Records;
public record ServerGenericFrame
    : IServerFrame<Dictionary<string, object>> {
    required public Dictionary<string, object> Estela { get; init; }
    required public Guid Tracer { get; init; }
}
