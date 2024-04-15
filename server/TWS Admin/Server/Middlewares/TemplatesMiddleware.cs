using System.Net;
using System.Text.Json;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Contracts.Exceptions.Interfaces;
using Foundation.Exceptions.Servers;

using Server.Templates;
using Server.Templates.Exposures;

using JObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Server.Middlewares;

public class TemplatesMiddleware
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        bool isSucceded = false;
        int StatusCode = (int)HttpStatusCode.Conflict;
        FailureTemplate<IException<IXFailure>, IGenericFailure>? Template = null;
        Exception? CriticalUnderivation = null;
        Guid Tracer;
        try {
            Tracer = Guid.Parse(context.TraceIdentifier);
        } catch {
            Tracer = Guid.NewGuid();
            context.TraceIdentifier = Tracer.ToString();
        }
        using MemoryStream bufferingStream = new();
        try {
            context.Response.Body = bufferingStream;
            await next.Invoke(context);
            isSucceded = true;
        } catch (BException DefinedX) when (DefinedX is IException<IXFailure> CastedX) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Template = new(CastedX, Tracer);
        } catch (BException DefinedXNoFailure) {
            StatusCode = (int)HttpStatusCode.BadRequest;
            XGenericException Generic = new(DefinedXNoFailure);
            Template = new(Generic, Tracer);
        } catch (Exception UndefinedX) {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            XUndefined DefinedX = new(UndefinedX);
            IException<IXFailure> CastedX = DefinedX.GenerateDerivation();
            Template = new(CastedX, Tracer);
        } finally {
            HttpResponse Response = context.Response;
            if (!Response.HasStarted) {
                bufferingStream.Seek(0, SeekOrigin.Begin);
                string bufferContent = await new StreamReader(bufferingStream).ReadToEndAsync();
                string encodedContent = "";

                if (isSucceded) {
                    JObject? decodedContent = JsonSerializer.Deserialize<JObject>(bufferContent);
                    if (decodedContent != null) {
                        SuccessTemplate<JObject> SuccessTemplate = new(decodedContent, Guid.Parse(context.TraceIdentifier));
                        SuccessExposure<JObject> Expoure = SuccessTemplate.GenerateExposure();
                        encodedContent = JsonSerializer.Serialize(Expoure);
                    } else {
                        Exception UnthrownExceptionRecognized = new("Response content received null on templating proccess");
                        XServer SystemException = new(UnthrownExceptionRecognized);
                        Response.StatusCode = (int)HttpStatusCode.Conflict;
                        Template = new(SystemException, Tracer);
                        FailureExposure<IGenericFailure> Exposure = Template.GenerateExposure();
                        encodedContent = JsonSerializer.Serialize(Exposure);
                    }
                } else {
                    context.Response.StatusCode = StatusCode;
                    if (Template is null) {
                        IException<IXFailure> ExceptionContract = new XDerivation(CriticalUnderivation).GenerateDerivation();
                        Template = new(ExceptionContract, Tracer);
                    }

                    FailureExposure<IGenericFailure> Exposure = Template.GenerateExposure();
                    encodedContent = JsonSerializer.Serialize(Exposure);
                }

                Response.ContentType = "application/json";

                MemoryStream swapperBuffer = new();
                StreamWriter writer = new(swapperBuffer);
                await writer.WriteAsync(encodedContent);
                await writer.FlushAsync();
                swapperBuffer.Seek(0, SeekOrigin.Begin);
                Response.Body = swapperBuffer;
            }
        }
    }
}