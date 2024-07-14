using System.Net;
using System.Text.Json;

using CSM_Foundation.Core.Exceptions;
using CSM_Foundation.Server.Interfaces;
using CSM_Foundation.Server.Records;

using Microsoft.AspNetCore.Http.Extensions;

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
                _ = bufferingStream.Seek(0, SeekOrigin.Begin);
                string encodedContent = "";
                if (failure is not null) {
                    ServerExceptionPublish exPublish = failure.Publish();

                    FailureFrame frame = new() {
                        Tracer = Tracer,
                        Estela = exPublish,
                    };

                    Response.StatusCode = (int)failure.Status;
                    encodedContent = JsonSerializer.Serialize(frame);
                } else if (Response.StatusCode != 200) {
                    Stream resolutionStream = Response.Body;

                    switch (Response.StatusCode) {
                        case 204:
                            encodedContent = "{}";
                            break;
                        case 405: {
                                ServerExceptionPublish publish = new XSystem(new MethodAccessException()).Publish();
                                FailureFrame frame = new() {
                                    Tracer = Tracer,
                                    Estela = publish,
                                };
                                encodedContent = JsonSerializer.Serialize(frame);
                            }
                            break;
                        case 404: {
                                ServerExceptionPublish publish = new XSystem(new Exception($"{context.Request.GetDisplayUrl()} not found")).Publish();
                                FailureFrame frame = new() {
                                    Tracer = Tracer,
                                    Estela = publish,
                                };

                                encodedContent = JsonSerializer.Serialize(frame);
                            }
                            break;
                        default:
                            encodedContent = JsonSerializer.Serialize(resolutionStream);
                            break;
                    }
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
                _ = swapperBuffer.Seek(0, SeekOrigin.Begin);
                Response.Body = swapperBuffer;
            }
        }
    }
}