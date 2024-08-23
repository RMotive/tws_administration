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
            Range = 10,
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
                Status = 1,
                Policy = "P232Policy" + iterationTag,
                Expiration = date,
                Country = "MEX"
            };
            Situation situation = new() {
                Name = "Situational test " + iterationTag,
                Description = "Description test " + iterationTag
            };
            Maintenance maintenance = new() {
                Status = 1,
                Anual = date,
                Trimestral = date,
            };
            Sct sct = new() {
                Status = 1,
                Type = "TypT14",
                Number = "NumberSCTTesting value" + iterationTag,
                Configuration = "Conf" + iterationTag
            };
            Address address = new() {
                Street = "Main street " + iterationTag,
                Country = "USA"
            };

            Usdot usdot = new() {
                Status = 1,
                Mc = "mc- " + iterationTag,
                Scac = "s" + iterationTag
            };

            Approach contact = new() {
                Status = 1,
                Email = "mail@test.com " + iterationTag
            };

            Carrier carrier = new() {
                Status = 1,
                Name = "Carrier test " + iterationTag,
                Approach = 0,
                Address = 0,
                ApproachNavigation = contact,
                AddressNavigation = address,
                SctNavigation = sct,
                UsdotNavigation = usdot
            };


            string vin = "VINnumber test" + iterationTag;
            string motor = "Motor number " + iterationTag;
            string economic = "Economic #n " + iterationTag;
            Plate plateMX = new() {
                Status = 1,
                Identifier = "mxPlate" + iterationTag,
                State = "BAC",
                Country = "MXN",
                Expiration = date,
                Truck = 2
            };
            Plate plateUSA = new() {
                Status = 1,
                Identifier = "usaPlate" + iterationTag,
                State = "CaA",
                Country = "USA",
                Expiration = date,
                Truck = 2
            };

            List<Plate> plateList = [plateMX, plateUSA];
            //Truck truck = new() {
            //    Status = 1,
            //    Vin = vin,
            //    Motor = motor,
            //    Economic = economic,
            //    Maintenance = 0,
            //    ManufacturerNavigation = manufacturer,
            //    InsuranceNavigation = insurance,
            //    MaintenanceNavigation = maintenance,
            //    SituationNavigation = situation,
            //    Carrier = 0,
            //    CarrierNavigation = carrier,
            //    Plates = plateList,
            //};
            //mockList.Add(truck);
        }

        //(HttpStatusCode Status, ServerGenericFrame response) = await Post("Create", mockList, true);
        //Assert.Equal(HttpStatusCode.OK, Status);

    }
    [Fact]
    public async Task Update() {
        //#region First (Correctly creates when doesn't exist)
        //{
        //    DateOnly date = new(2024, 12, 12);
        //    string testTag = Guid.NewGuid().ToString()[..3];
        //    Manufacturer manufacturer = new() {
        //        Model = "X23",
        //        Brand = "SCANIA TEST" + testTag,
        //        Year = date
        //    };
        //    Situation situation = new() {
        //        Name = "Situational test " + testTag,
        //        Description = "Description test " + testTag
        //    };
        //    Maintenance maintenace = new() {
        //        Status = 1,
        //        Anual = date,
        //        Trimestral = date,
        //    };
        //    Sct sct = new() {
        //        Status = 1,
        //        Type = "TypT14",
        //        Number = "NumberSCTTesting value" + testTag,
        //        Configuration = "Conf" + testTag
        //    };

        //    Plate plateMX = new() {
        //        Status = 1,
        //        Identifier = "mxPlate" + testTag,
        //        State = "BAC",
        //        Country = "MXN",
        //        Expiration = date,
        //        Truck = 2
        //    };
        //    Plate plateUSA = new() {
        //        Status = 1,
        //        Identifier = "usaPlate" + testTag,
        //        State = "CaA",
        //        Country = "USA",
        //        Expiration = date,
        //        Truck = 2
        //    };
        //    Address address = new() {
        //        Street = "Main street " + testTag,
        //        Country = "USA"
        //    };

        //    Usdot usdot = new() {
        //        Status = 1,
        //        Mc = "mc- " + testTag,
        //        Scac = "s" + testTag
        //    };

        //    Approach contact = new() {
        //        Status = 1,
        //        Email = "mail@test.com " + testTag
        //    };

        //    Carrier carrier = new() {
        //        Status = 1,
        //        Name = "Carrier test " + testTag,
        //        Approach = 0,
        //        Address = 0,
        //        ApproachNavigation = contact,
        //        AddressNavigation = address,
        //        SctNavigation = sct,
        //        UsdotNavigation = usdot
        //    };

        //    List<Plate> plateList = [plateMX, plateUSA];
        //    string vin = "VINnumber test" + testTag;
        //    string motor = "Motor number " + testTag;
        //    string economic = "Economic #n " + testTag;

        //    Truck mock = new() {
        //        Id = 0,
        //        Status = 1,
        //        Vin = vin,
        //        Motor = motor,
        //        Economic = economic,
        //        Manufacturer = 0,
        //        ManufacturerNavigation = manufacturer,
        //        MaintenanceNavigation = maintenace                         ,
        //        SituationNavigation = situation,
        //        Plates = plateList,
        //        Carrier = 0,
        //        CarrierNavigation = carrier,
        //    };

        //    (HttpStatusCode Status, ServerGenericFrame Respone) = await Post("Update", mock, true);

        //    Assert.Equal(HttpStatusCode.OK, Status);
        //    RecordUpdateOut<Truck> creationResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(Respone).Estela;

        //    Assert.Null(creationResult.Previous);

        //    Truck updated = creationResult.Updated;
        //    Assert.True(updated.Id > 0);
        //}
        //#endregion

        //#region Second (Updates an exist record)
        //{
        //    #region generate a new record
        //    DateOnly date = new(2024, 12, 12);
        //    string testTag = Guid.NewGuid().ToString()[..3];
        //    string testKey = "First";
        //    Manufacturer manufacturer = new() {
        //        Model = "X23",
        //        Brand = "SCANIA TEST" + testTag,
        //        Year = date
        //    };
        //    Situation situation = new() {
        //        Name = "Situational test " + testTag,
        //        Description = "Description test " + testTag
        //    };
        //    Maintenance maintenace = new() {
        //        Status = 1,
        //        Anual = date,
        //        Trimestral = date,
        //    };
        //    Sct sct = new() {
        //        Status = 1,
        //        Type = "TypT14",
        //        Number = "NumberSCTTesting value" + testTag,
        //        Configuration = "Conf" + testTag
        //    };
          
        //    Plate plateMX = new() {
        //        Status = 1,
        //        Identifier = "mxPlate" + testTag,
        //        State = "BAC",
        //        Country = "MXN",
        //        Expiration = date,
        //        Truck = 2
        //    };
        //    Plate plateUSA = new() {
        //        Status = 1,
        //        Identifier = "usaPlate" + testTag,
        //        State = "CaA",
        //        Country = "USA",
        //        Expiration = date,
        //        Truck = 2
        //    };
        //    Address address = new() {
        //        Street = "Main street " + testTag,
        //        Country = "USA"
        //    };

        //    Approach contact = new() {
        //        Status = 1,
        //        Email = "mail@test.com " + testTag
        //    };

        //    Carrier carrier = new() {
        //        Status = 1,
        //        Name = "Carrier test " + testTag,
        //        Approach = 0,
        //        Address = 0,
        //        ApproachNavigation = contact,
        //        AddressNavigation = address,
        //        SctNavigation = sct,
        //    };

        //    List<Plate> plateList = [plateMX, plateUSA];
        //    string vin = "VINnumber test" + testTag;
        //    string motor = "Motor number " + testTag;
        //    string economic = "Economic #n " + testTag;

        //    Truck mock = new() {
        //        Id = 0,
        //        Status = 1,
        //        Economic = economic,
        //        Vin = vin,
        //        Motor = motor,
        //        Manufacturer = 0,
        //        ManufacturerNavigation = manufacturer,
        //        MaintenanceNavigation = maintenace,
        //        SituationNavigation = situation,
        //        Plates = plateList,
        //        Carrier = 0,
        //        CarrierNavigation = carrier,
        //    };

        //    (HttpStatusCode Status, ServerGenericFrame Response) = await Post("Update", mock, true);

        //    Assert.Equal(HttpStatusCode.OK, Status);

        //    RecordUpdateOut<Truck> creationResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(Response).Estela;
        //    Assert.Null(creationResult.Previous);

        //    Truck creationRecord = creationResult.Updated;
        //    Assert.Multiple([
        //        () => Assert.True(creationRecord.Id > 0),
        //        () => Assert.Equal(mock.Vin, creationRecord.Vin),
        //        () => Assert.Equal(mock.Motor, creationRecord.Motor),

        //    ]);
        //    #endregion

        //    #region update only main properties
        //    // Validate main properties changes to the previous record.
        //    string modifiedVin = testKey + RandomUtils.String(12);
        //    string modifiedMotor = testKey + RandomUtils.String(11);
        //    mock = creationRecord;
         
        //    mock.Vin = modifiedVin;
        //    mock.Motor = modifiedMotor;

        //    (HttpStatusCode Status, ServerGenericFrame Response) updateResponse = await Post("Update", mock, true);

        //    Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
        //    RecordUpdateOut<Truck> updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

        //    Assert.NotNull(updateResult.Previous);

        //    Truck updateRecord = updateResult.Updated;
        //    Truck previousRecord = updateResult.Previous;
        //    Assert.Multiple([
        //        () => Assert.Equal(creationRecord.Id, updateRecord.Id),
        //        () => Assert.Equal(creationRecord.Manufacturer, updateRecord.Manufacturer),
        //        () => Assert.Equal(creationRecord.CarrierNavigation?.Id, updateRecord.CarrierNavigation?.Id),
        //        () => Assert.NotEqual(previousRecord.Vin, updateRecord.Vin),
        //        () => Assert.NotEqual(previousRecord.Motor, updateRecord.Motor)

        //    ]);
        //    #endregion

        //    #region update only navigation properties.
        //    // Validate nested properties changes to the previous record.
        //    creationRecord = updateRecord;
        //    mock = updateRecord;
        //    List<Plate> updatedPlates = [.. mock.Plates];
        //    updatedPlates[0].Identifier = RandomUtils.String(12);
        //    updatedPlates[1].Identifier = RandomUtils.String(12);
        //    mock.ManufacturerNavigation!.Brand = RandomUtils.String(15);
        //    mock.ManufacturerNavigation!.Model = RandomUtils.String(30);
        //    mock.ManufacturerNavigation!.Year = new DateOnly(1999, 12, 12);
        //    mock.SituationNavigation!.Name = RandomUtils.String(15);
        //    mock.Plates = updatedPlates;
        //    mock.CarrierNavigation!.Name = RandomUtils.String(10);
        //    mock.CarrierNavigation.SctNavigation!.Number = RandomUtils.String(25);
        //    updateResponse = await Post("Update", mock, true);

        //    Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
        //    updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

        //    Assert.NotNull(updateResult.Previous);

        //    updateRecord = updateResult.Updated;
        //    previousRecord = updateResult.Previous;
        //    updatedPlates = [.. updateRecord.Plates];
        //    Assert.Multiple([
        //        () => Assert.Equal(creationRecord.Id, updateRecord.Id),
        //        () => Assert.Equal(creationRecord.Manufacturer, updateRecord.Manufacturer),
        //        () => Assert.Equal(creationRecord.CarrierNavigation?.Id, updateRecord.CarrierNavigation?.Id),
        //        () => Assert.Equal(creationRecord.ManufacturerNavigation?.Id, updateRecord.ManufacturerNavigation?.Id),
        //        () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Brand, updateRecord.ManufacturerNavigation?.Brand),
        //        () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Year.ToString(), updateRecord.ManufacturerNavigation?.Year.ToString()),
        //        () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Model, updateRecord.ManufacturerNavigation?.Model),
        //        () => Assert.NotEqual(previousRecord.SituationNavigation?.Name, updateRecord.SituationNavigation?.Name),
        //        () => Assert.NotEqual(previousRecord.Plates.ToList()[0].Identifier, updateRecord.Plates.ToList()[0].Identifier),
        //        () => Assert.NotEqual(previousRecord.CarrierNavigation?.Name, updateRecord.CarrierNavigation?.Name),
        //        () => Assert.NotEqual(previousRecord.CarrierNavigation!.SctNavigation?.Number, updateRecord.CarrierNavigation!.SctNavigation?.Number),

        //    ]);
        //    #endregion

        //    #region update both, main properties and navigation properties.
        //    // Validate nested properties changes to the previous record.
        //    mock = updateRecord;
        //    mock.Vin = RandomUtils.String(17);
        //    mock.Motor = RandomUtils.String(16);
        //    updatedPlates[0].Identifier = RandomUtils.String(12);
        //    updatedPlates[1].Identifier = RandomUtils.String(12);
        //    mock.ManufacturerNavigation!.Brand = RandomUtils.String(15);
        //    mock.ManufacturerNavigation!.Model = RandomUtils.String(30);
        //    mock.ManufacturerNavigation!.Year = new DateOnly(200, 12, 12);
        //    mock.SituationNavigation!.Name = RandomUtils.String(15);
        //    mock.Plates = updatedPlates;
        //    updateResponse = await Post("Update", mock, true);

        //    Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
        //    updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

        //    Assert.NotNull(updateResult.Previous);

        //    updateRecord = updateResult.Updated;
        //    previousRecord = updateResult.Previous;

        //    updatedPlates = [.. updateRecord.Plates];
        //    Assert.Multiple([
        //        () => Assert.Equal(creationRecord.Id, updateRecord.Id),
        //        () => Assert.Equal(creationRecord.Manufacturer, updateRecord.Manufacturer),
        //        () => Assert.Equal(creationRecord.CarrierNavigation?.Id, updateRecord.CarrierNavigation?.Id),
        //        () => Assert.Equal(creationRecord.ManufacturerNavigation?.Id, updateRecord.ManufacturerNavigation?.Id),
        //        () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Brand, updateRecord.ManufacturerNavigation?.Brand),
        //        () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Year.ToString(), updateRecord.ManufacturerNavigation?.Year.ToString()),
        //        () => Assert.NotEqual(previousRecord.ManufacturerNavigation?.Model, updateRecord.ManufacturerNavigation?.Model),
        //        () => Assert.NotEqual(previousRecord.SituationNavigation?.Name, updateRecord.SituationNavigation?.Name),
        //        () => Assert.NotEqual(previousRecord.Plates.ToList()[0].Identifier, updateRecord.Plates.ToList()[0].Identifier),
        //    ]);
        //    #endregion

        //    #region Adding optional property.
        //    Insurance insurance = new() {
        //        Status = 1,
        //        Policy = "P232Policy" + testTag,
        //        Expiration = date,
        //        Country = "MEX"
        //    };

        //    Usdot usdot = new() {
        //        Status = 1,
        //        Mc = "mc- " + testTag,
        //        Scac = "s" + testTag
        //    };
        //    mock = updateRecord;
        //    mock.InsuranceNavigation = insurance;
        //    mock.CarrierNavigation!.UsdotNavigation = usdot;
        //    updateResponse = await Post("Update", mock, true);

        //    Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
        //    updateResult = Framing<SuccessFrame<RecordUpdateOut<Truck>>>(updateResponse.Response).Estela;

        //    Assert.NotNull(updateResult.Previous);

        //    updateRecord = updateResult.Updated;
        //    previousRecord = updateResult.Previous;

        //    Assert.Multiple([
        //        () => Assert.Equal(mock.Id, updateRecord.Id),
        //        () => Assert.Equal(mock.InsuranceNavigation!.Policy, updateRecord.InsuranceNavigation!.Policy),
        //        () => Assert.Equal(mock.CarrierNavigation!.UsdotNavigation.Mc, updateRecord.CarrierNavigation!.UsdotNavigation!.Mc),
        //        () => Assert.NotEqual(mock.CarrierNavigation!.UsdotNavigation.Id, updateRecord.CarrierNavigation!.UsdotNavigation!.Id),

        //    ]);
        //    #endregion


        //}
        //}
        //#endregion
    }

}