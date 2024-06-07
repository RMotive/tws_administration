using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using Foundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Bases;
using Server.Quality.Secrets;

using Xunit;

using PrivilegesFrame = Server.Middlewares.Frames.SuccessFrame<Customer.Managers.Records.Session>;

namespace Server.Quality.Quality.Controllers;

public class Q_SecurityController
    : BQ_CustomServerController {
    public Q_SecurityController(WebApplicationFactory<Program> hostFactory)
        : base("Security", hostFactory) { }

    [Fact]
    public async void Authenticate() {
        (HttpStatusCode Status, ServerGenericFrame Frame) fact = await Post("Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });
        Dictionary<string, object> estela = fact.Frame.Estela;
        if(fact.Status != HttpStatusCode.OK) {
            Assert.Fail($"Failed request with: {estela[nameof(ServerExceptionPublish.System)]} \ndue to: {estela[nameof(ServerExceptionPublish.Advise)]} \nTried with: {Account.Identity}");
        }
        PrivilegesFrame successFrame = Framing<PrivilegesFrame>(fact.Frame);
        Session session = successFrame.Estela;
        Assert.True(session.Wildcard, $"User {session.Identity} doesn't have wildcard enabled");
        Assert.Equal(Account.Identity, session.Identity);

        if(!session.Permits.Any(i => i.Name == "Quality")) {
            Assert.Fail($"Account {Account.Identity} doesn0't contain Quality permit");
        }
        Assert.Contains(session.Permits, i => i.Name == "Quality");
    }
}
