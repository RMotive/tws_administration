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
        bool isSucceded = false;
        int StatusCode = (int)HttpStatusCode.Conflict;
        FailureTemplate<IException<IExceptionExposure>, IGenericExceptionExposure>? Template = null;
        Exception? CriticalUnderivation = null;

        try {
            await next.Invoke(context);
        } catch (BException DefinedX) when (DefinedX is IException<IExceptionExposure> CastedX) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Template = new(CastedX);
            isSucceded = true;
        } catch (Exception UndefinedX) {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            XUndefined DefinedX = new(UndefinedX);
            IException<IExceptionExposure> CastedX = DefinedX.GenerateDerivation();
            if (CastedX is null)
                CriticalUnderivation = UndefinedX;
            else
                Template = new(CastedX);
        } finally {
            if(!isSucceded) {
                context.Response.StatusCode = StatusCode;
                if (Template is null) {
                    XDerivation DerivationException = new(CriticalUnderivation);
                    IException<IExceptionExposure> ExceptionContract = DerivationException.GenerateDerivation();
                    Template = new(ExceptionContract);
                }


                FailureExposure<IGenericExceptionExposure> Exposure = Template.GenerateExposure();

                await context.Response.WriteAsJsonAsync((object)Exposure);
            }
        }
    }
}
