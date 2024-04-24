using System.Net;
using System.Text.Json;

using Foundation.Server.Bases;
using Foundation.Server.Interfaces;
using Foundation.Server.Records;
using Foundation.Shared.Exceptions;
using Server.Middlewares.Frames;

namespace Server.Middlewares;

public class FramingMiddleware
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        Guid Tracer;
        try {
            Tracer = Guid.Parse(context.TraceIdentifier);
        } catch {
            Tracer = Guid.NewGuid();
            context.TraceIdentifier = Tracer.ToString();
        }


        using MemoryStream bufferingStream = new();

        IServerTransactionException? failure = null;
        try {
            context.Response.Body = bufferingStream;
            await next.Invoke(context);
        } catch (Exception ex) when (ex is IServerTransactionException Exception) {
            failure = Exception;
        } catch (Exception ex) {
            XSystem systemEx = new(ex);
            failure = systemEx;
        } finally {
            HttpResponse Response = context.Response;

            if (!Response.HasStarted) {
                bufferingStream.Seek(0, SeekOrigin.Begin);
                string encodedContent = "";
                if(failure is not null) {
                    ServerExceptionPublish exPublish = failure.Publish();

                    FailureFrame frame = new() {
                        Tracer = Tracer,
                        Estela = exPublish,
                    };

                    Response.StatusCode = (int)failure.Status;
                    encodedContent = JsonSerializer.Serialize(frame);
                } else {
                    Stream resolutionStream = Response.Body;
                    Dictionary<string, dynamic> resolution = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(resolutionStream)!;

                    SuccessFrame<Dictionary<string, dynamic>> frame = new() { 
                        Tracer = Tracer,
                        Estela = resolution,
                    };

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    encodedContent = JsonSerializer.Serialize(frame);
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