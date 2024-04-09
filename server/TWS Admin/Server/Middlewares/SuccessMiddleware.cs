using System.Text.Json;

using Foundation.Contracts.Server.Interfaces;

using Server.Templates;

using JObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Server.Middlewares;
public class SuccessMiddleware
    : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        context.Request.EnableBuffering();
        using MemoryStream MemoryStream = new();
        context.Response.Body = MemoryStream;
        await next(context);
        if(context.Response.StatusCode != 200) return;

        MemoryStream.Position = 0;
        JObject? responseContent = JsonSerializer.Deserialize<JObject>(MemoryStream);
        MemoryStream.Position = 0;
        SuccessTemplate<JObject?> Template = new(responseContent) {
            Tracer = Guid.Parse(context.TraceIdentifier),
        };

        ITemplateExposure<JObject?> Exposure = Template.GenerateExposure();
        string serializedBody = JsonSerializer.Serialize(Exposure);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(serializedBody);
    }
}
