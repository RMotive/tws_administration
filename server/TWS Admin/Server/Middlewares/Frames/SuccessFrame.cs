using CSMFoundation.Servers.Interfaces;

namespace Server.Middlewares.Frames;

public class SuccessFrame<TSuccess>
    : IServerFrame<TSuccess> {

    required public Guid Tracer { get; init; }
    required public TSuccess Estela { get; init; }
}
