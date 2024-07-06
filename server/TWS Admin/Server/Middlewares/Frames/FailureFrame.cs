using CSMFoundation.Server.Records;
using CSMFoundation.Servers.Interfaces;

namespace Server.Middlewares.Frames;

public class FailureFrame
    : IServerFrame<ServerExceptionPublish> {
    required public Guid Tracer { get; init; }
    required public ServerExceptionPublish Estela { get; init; }
}
