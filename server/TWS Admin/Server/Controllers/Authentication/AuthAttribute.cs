using Customer.Managers;

using Foundation.Server.Exceptions;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Server.Controllers.Authentication;

[AttributeUsage(AttributeTargets.Method)]
public class AuthAttribute
    : Attribute, IAuthorizationFilter {
    const string AUTH_TOKEN_KEY = "CSMAuth";

    readonly SessionsManager Sessions;
    readonly string[] Permits;

    public AuthAttribute(string[] Permits) {
        this.Permits = Permits;
        Sessions = SessionsManager.Manager;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
        StringValues headers = context.HttpContext.Request.Headers.Authorization;
        string authHedaer = headers
            .Where(i => i is not null && i.Contains(AUTH_TOKEN_KEY))
            .FirstOrDefault() 
            ?? throw new XAuth(XAuthSituation.Lack);

        string token = authHedaer.Split(' ')[1];
        if(Guid.TryParse(token, out Guid tokenGuid)) {
            if(Sessions.EvaluateWildcard(token)) 
                return;

            // TODO: Implement permits search

        } else throw new XAuth(XAuthSituation.Format);
    }
}
