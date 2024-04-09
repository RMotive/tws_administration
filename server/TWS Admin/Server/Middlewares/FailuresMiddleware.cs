using System.Net;
using System.Text.Json;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Exceptions.Servers;
using Foundation.Managers;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

using Server.Templates;
using Server.Templates.Exposures;

namespace Server.Middlewares;

public class FailuresMiddleware
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        bool isSucceded = false;
        int StatusCode = (int)HttpStatusCode.Conflict;
        FailureTemplate<IException<IXFailure>, IGenericFailure>? Template = null;
        Exception? CriticalUnderivation = null;

        try {
            await next.Invoke(context);
            isSucceded = true;
        } catch (BException DefinedX) when (DefinedX is IException<IXFailure> CastedX) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Template = new(CastedX) {
                Tracer = Guid.Parse(context.TraceIdentifier),
            };
        } catch (BException DefinedXNoFailure) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            XGenericException Generic = new(DefinedXNoFailure);
            Template = new (Generic);
        } catch (Exception UndefinedX) {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            XUndefined DefinedX = new(UndefinedX);
            IException<IXFailure> CastedX = DefinedX.GenerateDerivation();
            Template = new(CastedX) {
                Tracer = Guid.Parse(context.TraceIdentifier),
            };
        } finally {
            if(!isSucceded && !context.Response.HasStarted) {
                context.Response.StatusCode = StatusCode;
                if (Template is null) {
                    IException<IXFailure> ExceptionContract = new XDerivation(CriticalUnderivation).GenerateDerivation();
                    Template = new(ExceptionContract) {
                        Tracer = Guid.Parse(context.TraceIdentifier),
                    };
                }


                FailureExposure<IGenericFailure> Exposure = Template.GenerateExposure();
                string serializedBody = JsonSerializer.Serialize(Exposure);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(serializedBody);
            }
        }
    }
}
