
using Customer.Services.Records;
using Foundation.Migrations.Records;
using Foundation.Server.Records;
using Foundation.Servers.Quality.Bases;
using Microsoft.AspNetCore.Mvc.Testing;
using Server.Middlewares.Frames;
using System.Net;
using TWS_Business.Sets;
using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = Foundation.Migrations.Records.MigrationView<TWS_Business.Sets.Plate>;


namespace Server.Quality.Controllers;
public class Q_PlatesController : BQ_ServerController<Program> {
    private class Frame : SuccessFrame<View> { }


    public Q_PlatesController(WebApplicationFactory<Program> hostFactory)
        : base("Plates", hostFactory) {
    }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, SuccessFrame<Privileges> Response) = await XPost<SuccessFrame<Privileges>>("Security/Authenticate", new Credentials {
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
        DateOnly year = new(2024, 12, 12);

        //Manufacturer manufacturer = new() {
        //    Model = "X23 Plate Test",
        //    Brand = "SCANIA",
        //    Year = year
        //};

        //Truck truck = new() {
        //    Vin = "VIN plate test 1",
        //    Manufacturer = 1004,
        //    Motor = "Motor plate T1",
        //    ManufacturerNavigation = manufacturer
        //};
        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("Create", new Plate() {
            Identifier = "TMEX2323EST#",
            State = "BC",
            Country = "MXN",
            Expiration = year,
            Truck = 1,
        }, true);

        Assert.Equal(HttpStatusCode.OK, fact.Status);

    }
}