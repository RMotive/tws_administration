using Foundation.Server.Records;
using Foundation.Servers.Interfaces;

namespace Server.Templates;

public class FailureFrame
    : IServerFrame<ServerExceptionPublish> {
    required public Guid Tracer { get; init; }
    required public ServerExceptionPublish Estela { get; init; }
}
