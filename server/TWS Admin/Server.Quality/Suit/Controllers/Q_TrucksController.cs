using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using Foundation.Migrations.Records;
using Foundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;
using Server.Quality.Bases;

using TWS_Business.Sets;

using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = Foundation.Migrations.Records.MigrationView<TWS_Business.Sets.Truck>;

namespace Server.Quality.Controllers;
public class Q_TrucksController : BQ_CustomServerController {

    public Q_TrucksController(WebApplicationFactory<Program> hostFactory) : base("Trucks", hostFactory) {
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
    public async Task View() {
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
    public async Task Create() {
        DateOnly date = new(2024, 12, 12);

        string testTag = Guid.NewGuid().ToString()[..3];
        Manufacturer manufacturer = new() {
            Model = "X23",
            Brand = "SCANIA TEST" + testTag,
            Year = date
        };
        Insurance insurance = new() {
            Policy = "P232Policy" + testTag,
            Expiration = date,
            Country = "MEX"
        };

        Maintenance maintenace = new() {
            Anual = date,
            Trimestral = date,
        };
        Sct sct = new() {
            Type = "TypT14",
            Number = "NumberSCTTesting value" + testTag,
            Configuration = "Conf" + testTag
        };
        Situation situation = new() {
            Name = "Situational test " + testTag,
            Description = "Description test " + testTag
        };
        Plate plateMX = new() {
            Identifier = "mxPlate" + testTag,
            State = "BAC",
            Country = "MXN",
            Expiration = date,
            Truck = 2
        };
        Plate plateUSA = new() {
            Identifier = "usaPlate" + testTag,
            State = "CaA",
            Country = "USA",
            Expiration = date,
            Truck = 2
        };

        List<Plate> plateList = [plateMX, plateUSA];
        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("Create", new TruckAssembly {
            Vin = "VINnumber test" + testTag,
            Motor = "Motor number " + testTag,
            Manufacturer = manufacturer,
            Insurance = insurance,
            Maintenance = maintenace,
            Sct = sct,
            Situation = situation,
            Plates = plateList,
        }, true);

        fact.Response.Estela.TryGetValue("Advise", out object? value);
        Assert.Null(value);
        Assert.Equal(HttpStatusCode.OK, fact.Status);

    }
}