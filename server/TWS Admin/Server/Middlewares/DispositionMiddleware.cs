
using CSMFoundation.Migration.Interfaces;

using CSMFoundation.Server.Exceptions;

using Microsoft.Extensions.Primitives;

using Server.Managers;

namespace Server.Middlewares;

public class DispositionMiddleware : IMiddleware {
    const string DISP_HEAD_KEY = "CSMDisposition";

    const string DISP_HEAD_VALUE = "Quality";

    readonly DispositionManager Disposer;

    public DispositionMiddleware(IMigrationDisposer Disposer) {
        this.Disposer = (DispositionManager)Disposer;
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

        Disposer.Status(Activate);
        await next(context);
    }
}