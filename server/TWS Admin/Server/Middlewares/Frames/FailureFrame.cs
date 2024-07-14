using CSM_Foundation.Server.Interfaces;
using CSM_Foundation.Server.Records;

namespace Server.Middlewares.Frames;

public class FailureFrame
    : IServerFrame<ServerExceptionPublish> {
    public required Guid Tracer { get; init; }
    public required ServerExceptionPublish Estela { get; init; }
}
