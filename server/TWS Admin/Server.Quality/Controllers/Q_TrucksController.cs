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
using View = Foundation.Migrations.Records.MigrationView<TWS_Business.Sets.Truck>;

namespace Server.Quality.Controllers;
public class Q_TrucksController : BQ_ServerController<Program> {
    private class Frame : SuccessFrame<View> { }


    public Q_TrucksController(WebApplicationFactory<Program> hostFactory)
        : base("Trucks", hostFactory) {
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
            Range = 2,
            Retroactive = false,
        }, true);

        Assert.Equal(HttpStatusCode.OK, fact.Status);

        View Estela = Framing<SuccessFrame<View>>(fact.Response).Estela;
        Assert.True(Estela.Sets.Length > 0);
        Assert.Equal(1, Estela.Page);
        Assert.True(Estela.Pages > 0);
    }

    [Fact]
    public async void Assembly() {
        DateOnly year = new(2024, 12, 12);
        Manufacturer manufacturer = new() {
            Model = "X23",
            Brand = "SCANIA",
            Year = year
        };
        Plate plateMX = new() {
            Identifier = "mxPlate1",
            State = "BC",
            Country = "MXN",
            Expiration = year,
            Truck = 0
        };
        Plate plateUSA = new() {
            Identifier = "usaPlate1",
            State = "CA",
            Country = "USA",
            Expiration = year,
            Truck = 0
        };
        //List<Plate> plateList = [plateMX, plateUSA];
        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("Assembly", new TruckAssembly {
            Vin = "VIN number test 10",
            Motor = "Motor number T10",
            ManufacturerPointer = 2,
        }, true);

        Assert.Equal(HttpStatusCode.OK, fact.Status);

    }
}