
using Foundation.Server.Exceptions;

using Microsoft.Extensions.Primitives;

using Server.Managers;

namespace Server.Middlewares;

public class DispositionMiddleware : IMiddleware {
    const string DISP_HEAD_KEY = "CSMDisposition";

    const string DISP_HEAD_VALUE = "Quality";

    readonly DispositionManager Manager;

    public DispositionMiddleware(DispositionManager Manager) {
        this.Manager = Manager;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        HttpRequest request = context.Request;

        StringValues headers = request.Headers[DISP_HEAD_KEY];

        bool Activate = false;
        if (headers.Count > 0) {
            if (!headers.Contains(DISP_HEAD_VALUE))
                throw new XDisposition(XDispositionSituation.Value);

            Activate = true;
        }
        Manager.Status(Activate);
        await next(context);
    }
}