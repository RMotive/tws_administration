

using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using Foundation.Migrations.Records;
using Foundation.Server.Records;
using Foundation.Servers.Quality.Bases;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;

using TWS_Business.Sets;

using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = Foundation.Migrations.Records.MigrationView<TWS_Business.Sets.Manufacturer>;

namespace Server.Quality.Controllers;
public class Q_ManufacturerController
    : BQ_ServerController<Program> {

    private class Frame : SuccessFrame<View> { }

    public Q_ManufacturerController(WebApplicationFactory<Program> hostFactory)
        : base("Manufacturers", hostFactory) {
    }
    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, SuccessFrame<Session> Response) = await XPost<SuccessFrame<Session>, Credentials>("Security/Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        if (Status != HttpStatusCode.OK)
            throw new ArgumentNullException(nameof(Status));
        return Response.Estela.Token.ToString();
    }
    [Fact]
    public async void View() {
        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("View", new MigrationViewOptions {
            Page = 1,
            Range = 10,
            Retroactive = false,
        }, true);

        Assert.Equal(HttpStatusCode.OK, fact.Status);

        View Estela = Framing<SuccessFrame<View>>(fact.Response).Estela;
        Assert.True(Estela.Sets.Length > 0);
        Assert.Equal(1, Estela.Page);
        Assert.True(Estela.Pages > 0);
    }

    [Fact]
    public async void Create() {
        DateOnly date = new(2024, 10, 10);

        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("Create", new Manufacturer() {
            Model = "X23",
            Brand = "SCANIA ctr T1",
            Year = date
        }, true);

        fact.Response.Estela.TryGetValue("Advise", out object? value);
        Assert.Null(value);
        Assert.Equal(HttpStatusCode.OK, fact.Status);
    }

}
