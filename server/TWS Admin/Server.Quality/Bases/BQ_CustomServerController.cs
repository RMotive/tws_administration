using System.Net;

using Customer.Services.Records;

using Foundation.Servers.Quality.Bases;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Secrets;

using PrivilegesFrame = Server.Middlewares.Frames.SuccessFrame<Customer.Managers.Records.Session>;

namespace Server.Quality.Bases;
public abstract class BQ_CustomServerController
    : BQ_ServerController<Program> {

    protected BQ_CustomServerController(string Service, WebApplicationFactory<Program> hostFactory) : base(Service, hostFactory) { }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, PrivilegesFrame Response) = await XPost<PrivilegesFrame, Credentials>("Security/Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        if (Status != HttpStatusCode.OK)
            throw new ArgumentNullException(nameof(Status));
        if (!Response.Estela.Permits.Any(i => i.Name == "Quality" && i.Solution == 1))
            throw new ArgumentException($"The credentials ({Account.Identity}) doesn't have quality privileges at the solution. \n Please check: Secrets/Account.cs");

        return Response.Estela.Token.ToString();
    }
}
