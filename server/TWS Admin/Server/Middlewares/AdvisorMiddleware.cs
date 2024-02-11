
using Foundation.Exceptions.Servers;
using Foundation.Managers;

namespace Server.Middlewares;

public class AdvisorMiddleware 
    : IMiddleware {

    public AdvisorMiddleware() { }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            AdvisorManager.Announce($"Received server request from ({context.Connection.RemoteIpAddress}:{context.Connection.RemotePort})");
            await next.Invoke(context);
        } catch (Exception XU) {
            XUndefined XCritical = new(XU);
            AdvisorManager.Exception(XCritical);
        }
    }
}
