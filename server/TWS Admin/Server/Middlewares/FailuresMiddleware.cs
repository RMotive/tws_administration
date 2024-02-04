using System.Net;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Exceptions.Servers;

using Server.Templates;
using Server.Templates.Exposures;

namespace Server.Middlewares;

public class FailuresMiddleware 
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        int StatusCode = (int)HttpStatusCode.Conflict;
        FailureTemplate<IException<IExceptionExposure>>? Template = null;
        Exception? CriticalUnderivation = null;

        try {
            await next.Invoke(context);
        } catch(BException DefinedX) when (DefinedX is IException<IExceptionExposure> CastedX) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Template = new(CastedX);
        } catch (Exception UndefinedX) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            XUndefined DefinedX = new(UndefinedX);
            IException<IExceptionExposure> CastedX = DefinedX.GenerateDerivation();
            if(CastedX is null) 
                CriticalUnderivation = UndefinedX;
            else 
                Template = new(CastedX);
        } finally {
            context.Response.StatusCode = StatusCode;
            if(Template is null) {
                XDerivation DerivationException = new();
                IException<IExceptionExposure> ExceptionContract = DerivationException.GenerateDerivation();
                Template = new(ExceptionContract);
            }

            FailureExposure<IExceptionExposure> Exposure = Template.GenerateExposure();

            await context.Response.WriteAsJsonAsync(Exposure);
        }
    }
}
