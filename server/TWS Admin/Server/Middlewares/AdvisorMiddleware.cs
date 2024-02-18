
using System.Text.Json;

using Foundation.Exceptions.Servers;
using Foundation.Managers;

using JObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Server.Middlewares;

public class AdvisorMiddleware 
    : IMiddleware {

    public AdvisorMiddleware() { }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        Stream primaryBody = context.Response.Body;
        try {
            AdvisorManager.Announce(
                $"Received server request from ({context.Connection.RemoteIpAddress}:{context.Connection.RemotePort})", 
                new() {
                    {"Tracer", context.TraceIdentifier }
                }
            );
            context.Request.EnableBuffering();
            using MemoryStream memory = new();
            context.Response.Body = memory;
            await next(context);
            memory.Position = 0;
            JObject? responseContent = JsonSerializer.Deserialize<JObject>(memory); 
            memory.Position = 0;
            await memory.CopyToAsync(primaryBody);
            if(responseContent != null && responseContent.TryGetValue("Estela", out dynamic? value)) {
                JsonElement Estela = value;
                JObject? EstelaObject = Estela.Deserialize<JObject>();
                if (EstelaObject != null && EstelaObject.ContainsKey("Failure"))
                    AdvisorManager.Warning($"Reques served with failure", responseContent);
                else AdvisorManager.Success($"Request served successful", responseContent);
            } else {
                AdvisorManager.Success($"Request served successful", responseContent);
            }
        } catch (Exception XU) {
            XUndefined XCritical = new(XU);
            AdvisorManager.Exception(XCritical);
        } finally {
            context.Response.Body = primaryBody;
        }
    }
}   
