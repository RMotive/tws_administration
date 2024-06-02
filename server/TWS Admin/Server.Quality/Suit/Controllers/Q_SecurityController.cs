using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Bases;
using Server.Quality.Secrets;

using Xunit;

using PrivilegesFrame = Server.Middlewares.Frames.SuccessFrame<Customer.Managers.Records.Session>;

namespace Server.Quality.Controllers;

public class Q_SecurityController
    : BQ_CustomServerController {
    public Q_SecurityController(WebApplicationFactory<Program> hostFactory)
        : base("Security", hostFactory) { }

    [Fact]
    public async void Authenticate() {
        (HttpStatusCode Status, PrivilegesFrame Frame) fact = await Post<PrivilegesFrame, Credentials>("Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        PrivilegesFrame frame = fact.Frame;

        Assert.Equal(HttpStatusCode.OK, fact.Status);
        Assert.True(frame.Estela.Wildcard);
        Assert.Equal(Account.Identity, frame.Estela.Identity);


        Session privileges = fact.Frame.Estela;
        Assert.Contains(frame.Estela.Permits, i => i.Name == "Quality");
    }
}
