
using Foundation.Contracts.Exceptions;
using Foundation.Exceptions.Servers;

using Server.Templates;

namespace Server.Middlewares;

public class FailuresMiddleware 
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next.Invoke(context);
        } catch(BException CX) {
            FailureTemplate<BException> FailureTemplate = new(CX);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(FailureTemplate);
        } catch (Exception UX) {
            XServerFailure ServerFailure = new(UX);
            FailureTemplate<XServerFailure> FailureTemplate = new(ServerFailure);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(FailureTemplate);
        }
    }
}
