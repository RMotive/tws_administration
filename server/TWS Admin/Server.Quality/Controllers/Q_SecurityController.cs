using System.Net;

using Customer.Services.Records;

using Foundation.Server.Records;
using Foundation.Servers.Quality.Bases;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Secrets;

using Xunit;

using PrivilegesFrame = Server.Middlewares.Frames.SuccessFrame<Customer.Services.Records.Privileges>;

namespace Server.Quality.Controllers;

public class Q_SecurityController
    : BQ_ServerController<Program> {
    public Q_SecurityController(WebApplicationFactory<Program> hostFactory)
        : base("Security", hostFactory) { }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, PrivilegesFrame Response) = await Post<PrivilegesFrame, Credentials>("Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        if (Status != HttpStatusCode.OK)
            throw new ArgumentNullException(nameof(Status));
        return Response.Estela.Token.ToString();
    }

    [Fact]
    public async void Authenticate() {
        (HttpStatusCode Status, ServerGenericFrame Frame) fact = await Post("Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });


        Assert.Equal(HttpStatusCode.OK, fact.Status);
        PrivilegesFrame frame = Framing<PrivilegesFrame>(fact.Frame);

        Assert.True(frame.Estela.Wildcard);
        Assert.Equal(Account.Identity, frame.Estela.Identity);
    }
}
