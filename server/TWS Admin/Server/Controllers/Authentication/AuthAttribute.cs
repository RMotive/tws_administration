using CSM_Foundation.Server.Exceptions;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

using TWS_Customer.Managers;

namespace Server.Controllers.Authentication;

[AttributeUsage(AttributeTargets.Method)]
public class AuthAttribute
    : Attribute, IAuthorizationFilter {
    private const string AUTH_TOKEN_KEY = "CSMAuth";
    private readonly SessionsManager Sessions;
    private readonly string[] Permits;

    public AuthAttribute(string[] Permits) {
        this.Permits = Permits;
        Sessions = SessionsManager.Manager;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
        StringValues authHeader = context.HttpContext.Request.Headers.Authorization;
        IHeaderDictionary headers = context.HttpContext.Request.Headers;



        string authHedaer = authHeader
            .Where(i => i is not null && i.Contains(AUTH_TOKEN_KEY))
            .FirstOrDefault()
            ?? throw new XAuth(XAuthSituation.Lack);

        string token = authHedaer.Split(' ')[1];
        if (Guid.TryParse(token, out Guid tokenGuid)) {
            if (Sessions.EvaluateWildcard(token)) {
                return;
            }

            // TODO: Implement permits search
            foreach (string permit in Permits) {

            }

        } else {
            throw new XAuth(XAuthSituation.Format);
        }
    }
}
