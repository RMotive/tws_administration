
using Foundation.Exceptions.Servers;

using Server.Templates;

namespace Server.Middlewares;

public class FailuresMiddleware 
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next.Invoke(context);
        } catch (Exception UX) {
            XServerFailure ServerFailure = new();
            FailureTemplate<XServerFailure> FailureTemplate = new(ServerFailure);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(FailureTemplate);
        }
    }
}
