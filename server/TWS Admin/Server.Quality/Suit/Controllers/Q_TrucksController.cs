using System.Net;

using CSM_Foundation.Server.Records;
using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;
using Server.Quality.Bases;

using TWS_Business.Sets;

using TWS_Customer.Managers.Records;
using TWS_Customer.Services.Records;

using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = CSM_Foundation.Source.Models.Out.SetViewOut<TWS_Business.Sets.Truck>;

namespace Server.Quality.Suit.Controllers;
public class Q_TrucksController : BQ_CustomServerController {

    public Q_TrucksController(WebApplicationFactory<Program> hostFactory) : base("Trucks", hostFactory) {
    }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, SuccessFrame<Session> Response) = await XPost<SuccessFrame<Session>, Credentials>("Security/Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        return Status != HttpStatusCode.OK ? throw new ArgumentNullException(nameof(Status)) : Response.Estela.Token.ToString();
    }

    [Fact]
    public async Task View() {
        (HttpStatusCode Status, ServerGenericFrame Response) = await Post("View", new SetViewOptions {
            Page = 1,
            Range = 2,
            Retroactive = false,
        }, true);

        Assert.Equal(HttpStatusCode.OK, Status);

        View Estela = Framing<SuccessFrame<View>>(Response).Estela;
        Assert.True(Estela.Sets.Length > 0);
        Assert.Equal(1, Estela.Page);
        Assert.True(Estela.Pages > 0);
    }

    [Fact]
    public async Task Create() {
        DateOnly date = new(2024, 12, 12);
        List<Truck> mockList = new();
        string testTag = Guid.NewGuid().ToString()[..2];

        for (int i = 0; i < 3; i++) {
            string iterationTag = testTag + i;
            Manufacturer manufacturer = new() {
                Model = "X23",
                Brand = "SCANIA TEST" + iterationTag,
                Year = date
            };
            Insurance insurance = new() {
                Policy = "P232Policy" + iterationTag,
                Expiration = date,
                Country = "MEX"
            };

            Maintenance maintenace = new() {
                Anual = date,
                Trimestral = date,
            };
            Sct sct = new() {
                Type = "TypT14",
                Number = "NumberSCTTesting value" + iterationTag,
                Configuration = "Conf" + iterationTag
            };
            Situation situation = new() {
                Name = "Situational test " + iterationTag,
                Description = "Description test " + iterationTag
            };
            Plate plateMX = new() {
                Identifier = "mxPlate" + iterationTag,
                State = "BAC",
                Country = "MXN",
                Expiration = date,
                Truck = 2
            };
            Plate plateUSA = new() {
                Identifier = "usaPlate" + iterationTag,
                State = "CaA",
                Country = "USA",
                Expiration = date,
                Truck = 2
            };

            List<Plate> plateList = [plateMX, plateUSA];
            Truck truck = new() {
                Vin = "VINnumber test" + iterationTag,
                Motor = "Motor number " + iterationTag,
                Maintenance = 0,
                ManufacturerNavigation = manufacturer,
                InsuranceNavigation = insurance,
                MaintenanceNavigation = maintenace,
                SctNavigation = sct,
                SituationNavigation = situation,
                Plates = plateList,
            };
            mockList.Add(truck);
        }
        
        (HttpStatusCode Status, _) = await Post("Create", mockList, true);
        Assert.Equal(HttpStatusCode.OK, Status);

    }
}