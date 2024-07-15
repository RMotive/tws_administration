using Microsoft.Extensions.Hosting;

namespace CSM_Foundation.Server.Quality.Bases;
/// <summary>
///     Defines base behavior for <see cref="BQ_ServerMiddleware"/> implementations
///     that qualifies server [Middleware] operations and configurations.
/// </summary>
public abstract class BQ_ServerMiddleware {
    /// <summary>
    ///     Host manager to perform and test operations.
    /// </summary>
    protected IHostBuilder Host { get; private set; }
    /// <summary>
    ///     Configures the <see cref="Host"/> that the quality will handle.
    /// </summary>
    /// <returns></returns>
    protected abstract IHostBuilder Configuration();
    /// <summary>
    ///     Generates a new <see cref="BQ_ServerMiddleware"/> middleware qualifications .
    /// </summary>
    public BQ_ServerMiddleware() {
        Host = Configuration();
    }
}
