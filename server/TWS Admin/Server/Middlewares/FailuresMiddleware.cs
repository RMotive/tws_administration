
using Foundation.Contracts.Exceptions;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Exceptions.Servers;

using Server.Templates;

namespace Server.Middlewares;

public class FailuresMiddleware 
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next.Invoke(context);
        } catch(BException CX) {

        } catch (Exception UX) {

        }
    }
}
