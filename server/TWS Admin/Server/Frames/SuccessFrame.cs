using Foundation.Servers.Interfaces;

namespace Server.Templates;

public class SuccessFrame<TSuccess>
    : IServerFrame {

    required public Guid Tracer { get; init; }
    required public TSuccess Estela { get; init; }
}
