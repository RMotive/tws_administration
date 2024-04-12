
namespace Server.Middlewares;

public class AnalyticsMiddleware
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        context.TraceIdentifier = Guid.NewGuid().ToString();
        await next(context);
    }
}
