using System.Net;
using System.Numerics;

using CSM_Foundation.Core.Bases;
using CSM_Foundation.Core.Utils;
using CSM_Foundation.Server.Records;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
            Situation situation = new() {
                Name = "Situational test " + iterationTag,
                Description = "Description test " + iterationTag
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
    [Fact]
    public async Task Update() {
        #region First (Correctly creates when doesn't exist)
        {
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
            Truck truck = new() {
                Id = 0,
                Vin = "VINnumber test" + testTag,
                Motor = "Motor number " + testTag,
                Manufacturer = 0,
                ManufacturerNavigation = manufacturer,
                InsuranceNavigation = insurance,
                MaintenanceNavigation = maintenace,
                SctNavigation = sct,
                SituationNavigation = situation,
                Plates = plateList,
            };

            (HttpStatusCode Status, ServerGenericFrame Respone) = await Post("Update", truck, true);

            Assert.Equal(HttpStatusCode.OK, Status);
            RecordUpdateOut<Truck> creationResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(Respone).Estela;

            Assert.Null(creationResult.Previous);

            Truck updated = creationResult.Updated;
            Assert.True(updated.Id > 0);
        }
        #endregion

        #region Second (Updates an exist record)
        {
            #region generate a new record
            DateOnly date = new(2024, 12, 12);
            string testTag = Guid.NewGuid().ToString()[..3];
            string testKey = "First";
            Manufacturer manufacturer = new() {
                Model = "X23",
                Brand = "SCANIA TEST" + testTag,
                Year = date
            };
            Situation situation = new() {
                Name = "Situational test " + testTag,
                Description = "Description test " + testTag
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
            Truck mock = new() {
                Id = 0,
                Vin = "VINnumber test" + testTag,
                Motor = "Motor number " + testTag,
                Manufacturer = 0,
                ManufacturerNavigation = manufacturer,
                MaintenanceNavigation = maintenace,
                SctNavigation = sct,
                SituationNavigation = situation,
                Plates = plateList,
            };

            (HttpStatusCode Status, ServerGenericFrame Response) = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, Status);

            RecordUpdateOut<Truck> creationResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(Response).Estela;
            Assert.Null(creationResult.Previous);

            Truck creationRecord = creationResult.Updated;
            Assert.Multiple([
                () => Assert.True(creationRecord.Id > 0),
                () => Assert.Equal(mock.Vin, creationRecord.Vin),
                () => Assert.Equal(mock.Motor, creationRecord.Motor),

            ]);
            #endregion

            #region update only main properties
            // Validate main properties changes to the previous record.
            mock = creationRecord;
            mock.Vin = testKey + RandomUtils.String(12);
            mock.Motor = testKey + RandomUtils.String(11);

            (HttpStatusCode Status, ServerGenericFrame Response) updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            RecordUpdateOut<Truck> updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            Truck updateRecord = updateResult.Updated;
            Truck previousRecord = updateResult.Previous;
            Assert.Multiple([
                () => Assert.Equal(creationRecord.Id, updateRecord.Id),
                () => Assert.Equal(creationRecord.Manufacturer, updateRecord.Manufacturer),
                () => Assert.Equal(creationRecord.SctNavigation?.Id, updateRecord.SctNavigation?.Id),
                () => Assert.NotEqual(previousRecord.Vin, updateRecord.Vin),
                () => Assert.NotEqual(previousRecord.Motor, updateRecord.Motor)

            ]);
            #endregion

            #region update only navigation properties.
            // Validate nested properties changes to the previous record.
            creationRecord = updateRecord;
            mock = updateRecord;
            testKey = "Second";
            List<Plate> updatedPlates = [.. mock.Plates];
            updatedPlates[0].Identifier = RandomUtils.String(12);
            updatedPlates[1].Identifier = RandomUtils.String(12);
            mock.ManufacturerNavigation!.Brand = RandomUtils.String(15);
            mock.ManufacturerNavigation!.Model = RandomUtils.String(30);
            mock.ManufacturerNavigation!.Year = new DateOnly(1999, 12, 12);
            mock.SituationNavigation!.Name = RandomUtils.String(15);
            mock.Plates = updatedPlates;
            updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            updateRecord = updateResult.Updated;
            previousRecord = updateResult.Previous;
            //List<Plate> updatedPlates = [..updateRecord.Plates];
            Assert.Multiple([
                () => Assert.Equal(creationRecord.Id, updateRecord.Id),
                () => Assert.Equal(creationRecord.Manufacturer, updateRecord.Manufacturer),
                () => Assert.Equal(creationRecord.SctNavigation?.Id, updateRecord.SctNavigation?.Id),
                () => Assert.Equal(creationRecord.ManufacturerNavigation?.Id, updateRecord.ManufacturerNavigation?.Id),
                () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Brand, updateRecord.ManufacturerNavigation?.Brand),
                () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Year.ToString(), updateRecord.ManufacturerNavigation?.Year.ToString()),
                () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Model, updateRecord.ManufacturerNavigation?.Model),
                () => Assert.NotEqual(previousRecord.SituationNavigation?.Name, updateRecord.SituationNavigation?.Name),
                () => Assert.NotEqual(previousRecord.Plates.ToList()[0].Identifier, updateRecord.Plates.ToList()[0].Identifier),

            ]);
            #endregion

            #region update both, main properties and navigation properties.
            // Validate nested properties changes to the previous record.
            mock = updateRecord;
            mock.Vin = RandomUtils.String(17);
            mock.Motor = RandomUtils.String(16);
            updatedPlates[0].Identifier = RandomUtils.String(12);
            updatedPlates[1].Identifier = RandomUtils.String(12);
            mock.ManufacturerNavigation!.Brand = RandomUtils.String(15);
            mock.ManufacturerNavigation!.Model = RandomUtils.String(30);
            mock.ManufacturerNavigation!.Year = new DateOnly(200, 12, 12);
            mock.SituationNavigation!.Name = RandomUtils.String(15);
            mock.Plates = updatedPlates;
            updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            updateRecord = updateResult.Updated;
            previousRecord = updateResult.Previous;

            //List<Plate> updatedPlates = [..updateRecord.Plates];
            Assert.Multiple([
                () => Assert.Equal(creationRecord.Id, updateRecord.Id),
                () => Assert.Equal(creationRecord.Manufacturer, updateRecord.Manufacturer),
                () => Assert.Equal(creationRecord.SctNavigation?.Id, updateRecord.SctNavigation?.Id),
                () => Assert.Equal(creationRecord.ManufacturerNavigation?.Id, updateRecord.ManufacturerNavigation?.Id),
                () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Brand, updateRecord.ManufacturerNavigation?.Brand),
                () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Year.ToString(), updateRecord.ManufacturerNavigation?.Year.ToString()),
                () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Model, updateRecord.ManufacturerNavigation?.Model),
                () => Assert.NotEqual(previousRecord.SituationNavigation?.Name, updateRecord.SituationNavigation?.Name),
                () => Assert.NotEqual(previousRecord.Plates.ToList()[0].Identifier, updateRecord.Plates.ToList()[0].Identifier),
            ]);
            #endregion

            #region Adding optional property.
            Insurance insurance = new() {
                Policy = "P232Policy" + testTag,
                Expiration = date,
                Country = "MEX"
            };

            mock = updateRecord;
            mock.InsuranceNavigation = insurance;
            updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            updateRecord = updateResult.Updated;
            previousRecord = updateResult.Previous;

            Assert.Multiple([
                () => Assert.Equal(mock.Id, updateRecord.Id),
                () => Assert.Equal(mock.InsuranceNavigation!.Policy, updateRecord.InsuranceNavigation!.Policy),
            ]);
            #endregion


        }
        #endregion
    }

}