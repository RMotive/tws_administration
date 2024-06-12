using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using Foundation.Server.Records;
using Foundation.Servers.Quality.Bases;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Secrets;

using Xunit;

using PrivilegesFrame = Server.Middlewares.Frames.SuccessFrame<Customer.Managers.Records.Session>;

namespace Server.Quality.Bases;
public abstract class BQ_CustomServerController
    : BQ_ServerController<Program> {

    protected BQ_CustomServerController(string Service, WebApplicationFactory<Program> hostFactory) : base(Service, hostFactory) { }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, ServerGenericFrame Frame) fact = await XPost<ServerGenericFrame, Credentials>("Security/Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });
        Dictionary<string, object> estela = fact.Frame.Estela;
        if (fact.Status != HttpStatusCode.OK) {
            Assert.Fail($"Failed request with: {estela[nameof(ServerExceptionPublish.System)]} \ndue to: {estela[nameof(ServerExceptionPublish.Advise)]} \nTried with: {Account.Identity}");
        }
        PrivilegesFrame successFrame = Framing<PrivilegesFrame>(fact.Frame);
        Session session = successFrame.Estela;
        Assert.True(session.Wildcard, $"User {session.Identity} doesn't have wildcard enabled");
        Assert.Equal(Account.Identity, session.Identity);

        if (!session.Permits.Any(i => i.Reference == "AAA000001")) {
            Assert.Fail($"Account ({Account.Identity}) doesn't contain (Quality[AAA000001]) permit");
        }

        return session.Token.ToString();
    }
}
