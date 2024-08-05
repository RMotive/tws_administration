using System.Net;

using CSM_Foundation.Core.Utils;
using CSM_Foundation.Server.Records;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;
using Server.Quality.Bases;

using TWS_Business.Sets;

using Xunit;

using View = CSM_Foundation.Source.Models.Out.SetViewOut<TWS_Business.Sets.HPTruck>;

namespace Server.Quality.Suit.Controllers;


public class Q_HPTrucksController
    : BQ_CustomServerController {

    public Q_HPTrucksController(WebApplicationFactory<Program> hostFactory)
        : base("HPTrucks", hostFactory) {
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
        #region First (Correctly creates 3 Solutions)
        {
            DateOnly date = new(2024, 12, 12);
            List<HPTruck> mockList = new();
            string testTag = Guid.NewGuid().ToString()[..2];

            for (int i = 0; i < 3; i++) {
                string iterationTag = testTag + i;
                string vin = "VINnumber test" + iterationTag;
                string motor = "Motor number " + iterationTag;
                
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
                    Vin = vin,
                    Motor = motor,
                    Maintenance = 0,
                    Hp = 0,
                    Modified = DateTime.Now,
                    ManufacturerNavigation = manufacturer,
                    InsuranceNavigation = insurance,
                    MaintenanceNavigation = maintenace,
                    SctNavigation = sct,
                    SituationNavigation = situation,
                    Plates = plateList,
                };
                List<Truck> truckList = [truck];
                HPTruck hp = new() {
                    Creation = DateTime.Now,
                    Vin = vin,
                    Motor = motor,
                    Status = 1,
                    Trucks = truckList
                };
                mockList.Add(hp);
            }

            (HttpStatusCode Status, ServerGenericFrame response) = await Post("Create", mockList, true);
            Assert.Equal(HttpStatusCode.OK, Status);
        }
        #endregion
    }

    [Fact]
    public async Task Update() {
        #region First (Correctly creates when doesn't exist)
        {
            DateOnly date = new(2024, 12, 12);
            List<HPTruck> mockList = new();
            string testTag = Guid.NewGuid().ToString()[..3];
            string vin = "UpdateVIN test" + testTag;
            string motor = "Update_Motor " + testTag;

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
            Truck truck = new() {
                Vin = vin,
                Motor = motor,
                Maintenance = 0,
                Hp = 0,
                Modified = DateTime.Now,
                ManufacturerNavigation = manufacturer,
                InsuranceNavigation = insurance,
                MaintenanceNavigation = maintenace,
                SctNavigation = sct,
                SituationNavigation = situation,
                Plates = plateList,
            };
            List<Truck> truckList = [truck];
            HPTruck hp = new() {
                Creation = DateTime.Now,
                Vin = vin,
                Motor = motor,
                Status = 1,
                Trucks = truckList
            };
            
            (HttpStatusCode Status, ServerGenericFrame Respone) = await Post("Update", hp, true);

            Assert.Equal(HttpStatusCode.OK, Status);
            RecordUpdateOut<HPTruck> creationResult = Framing<SuccessFrame<RecordUpdateOut<HPTruck>>>(Respone).Estela;

            Assert.Null(creationResult.Previous);

            HPTruck updated = creationResult.Updated;
            Assert.True(updated.Id > 0);
        }
        #endregion

        #region Second (Updates an exist record)
        {
            DateOnly date = new(2024, 12, 12);
            List<HPTruck> mockList = new();
            string testTag = Guid.NewGuid().ToString()[..3];
            string vin = "UpdateVIN test" + testTag;
            string motor = "Update_Motor " + testTag;

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
            Situation situation = new() {
                Name = "Situational test " + testTag,
                Description = "Description test " + testTag
            };
            Maintenance maintenance = new() {
                Anual = date,
                Trimestral = date,
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
                Vin = vin,
                Motor = motor,
                Maintenance = 0,
                Hp = 0,
                Modified = DateTime.Now,
                ManufacturerNavigation = manufacturer,
                InsuranceNavigation = insurance,
                MaintenanceNavigation = maintenance,
                SituationNavigation = situation,
                Plates = plateList,
            };
            List<Truck> truckList = [truck];
            HPTruck mock = new() {
                Creation = DateTime.Now,
                Vin = vin,
                Motor = motor,
                Status = 1,
                Trucks = truckList
            };
            (HttpStatusCode Status, ServerGenericFrame Response) = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, Status);

            RecordUpdateOut<HPTruck> creationResult = Framing<SuccessFrame<RecordUpdateOut<HPTruck>>>(Response).Estela;
            Assert.Null(creationResult.Previous);
            Assert.Null(creationResult.Updated.Trucks.Last().Sct);
            Assert.Null(creationResult.Updated.Trucks.Last().SctNavigation);
            HPTruck creationRecord = creationResult.Updated;
            Assert.Multiple([
                () => Assert.True(creationRecord.Id > 0),
                () => Assert.Equal(mock.Vin, creationRecord.Vin),
                () => Assert.Equal(mock.Motor, creationRecord.Motor),
                () => Assert.Equal(mock.Creation, creationRecord.Creation),
            ]);
            string modifiedVin = RandomUtils.String(17);
            string modifiedMotor = RandomUtils.String(16);
            HPTruck creationRecordCopy = creationRecord.DeepCopy();
            List<Truck> newlist = [.. creationRecordCopy.Trucks];
            newlist.Last().Id = 0;
            newlist.Last().Vin = modifiedVin;
            newlist.Last().Motor = modifiedMotor;
            mock.Id = creationRecord.Id;
            mock.Vin = modifiedVin;
            mock.Motor = modifiedMotor;
            mock.Trucks = newlist;
            (HttpStatusCode Status, ServerGenericFrame Response) updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            RecordUpdateOut<HPTruck> updateResult = Framing<SuccessFrame<RecordUpdateOut<HPTruck>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            HPTruck updateRecord = updateResult.Updated;
            Assert.Multiple([
                () => Assert.Equal(creationRecord.Id, updateRecord.Id),
                () => Assert.Equal(creationRecord.Creation.ToString(), updateRecord.Creation.ToString()),
                () => Assert.Equal(modifiedVin, updateRecord.Trucks.Last().Vin),
                () => Assert.Equal(modifiedMotor, updateRecord.Trucks.Last().Motor),
                () => Assert.Equal(2, updateRecord.Trucks.Count),
                () => Assert.NotEqual(creationRecord.Vin, updateRecord.Vin),
                () => Assert.NotEqual(creationRecord.Motor, updateRecord.Motor),
                () => Assert.NotEqual(creationRecord.Trucks.Last().Vin, updateRecord.Trucks.Last().Vin),
                () => Assert.NotEqual(0, updateRecord.Trucks.Last().Id)
            ]);

            #region Update Plates and add new trucks to an existent record
           
            List<Truck> trucks = new List<Truck>();
            //Trucks to generate
            for (int i = 0; i < 3; i++) {
                modifiedVin = RandomUtils.String(16) + i.ToString();
                modifiedMotor = RandomUtils.String(15) + i.ToString();
                Truck temp = mock.DeepCopy().Trucks.Last();
                temp.Id = 0;
                temp.Vin = modifiedVin;
                temp.Motor = modifiedMotor;
                trucks.Add(temp);
            }

            // First Truck: Contains all available navigations fields and update only the vin & motor.
            // Second Truck: Contains only the required navigations.
            trucks[1].Maintenance = null;
            trucks[1].MaintenanceNavigation = null;
            trucks[1].Situation = null;
            trucks[1].SituationNavigation = null;
            trucks[1].Sct = null;
            trucks[1].SctNavigation = null;
            trucks[1].Insurance = null;
            trucks[1].InsuranceNavigation = null;
            // Third Truck: Updating the exiting plates and situation data
            List<Plate> modifiedPlates = trucks[2].Plates.ToList();
            string identifier = RandomUtils.String(9) + "MOD";
            modifiedPlates[0].Identifier = identifier;
            modifiedPlates[1].Identifier = identifier;
            trucks[2].Plates = modifiedPlates;
            trucks[2].SituationNavigation!.Name = RandomUtils.String(10) + " MOD";
           
            updateRecord.Trucks.Add(trucks[0]);
            updateRecord.Trucks.Add(trucks[1]);
            updateRecord.Trucks.Add(trucks[2]);
            mock.Trucks = [.. updateRecord.Trucks];
            mock.Vin = mock.Trucks.Last().Vin;
            mock.Motor = mock.Trucks.Last().Motor;
            
            mock.Trucks.First().Vin = RandomUtils.String(13) + " MOD";
            updateResponse = await Post("Update", mock, true);
            
            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            updateResult = Framing<SuccessFrame<RecordUpdateOut<HPTruck>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            updateRecord = updateResult.Updated;
            List<Truck> updatedTrucks = updateRecord.Trucks.ToList();
            List<Truck> previousTrucks = mock.Trucks.ToList();
            

            Assert.Multiple([
                () => Assert.Equal(mock.Id, updateRecord.Id),
                () => Assert.Equal(mock.Creation.ToString(), updateRecord.Creation.ToString()),
                () => Assert.Equal(mock.Vin, updateRecord.Trucks.Last().Vin),
                () => Assert.Equal(mock.Motor, updateRecord.Trucks.Last().Motor),
                () => Assert.Equal(mock.Vin, updateRecord.Vin),
                () => Assert.Equal(mock.Motor, updateRecord.Motor),
                () => Assert.Equal(5, updateRecord.Trucks.Count),
                () => Assert.Equal(previousTrucks[4].Plates.First().Identifier, updatedTrucks[4].Plates.First().Identifier),
                () => Assert.Equal(mock.Trucks.Last().Vin, updateRecord.Trucks.Last().Vin),
                () => Assert.Equal(mock.Trucks.Last().SituationNavigation!.Name, updateRecord.Trucks.Last().SituationNavigation!.Name),
                () => Assert.Equal(mock.Trucks.First().Vin, updateRecord.Trucks.First().Vin),
                () => Assert.NotEqual(0, updateRecord.Trucks.Last().Id),
                () => Assert.False(updatedTrucks[3].Maintenance > 0),
                () => Assert.False(updatedTrucks[3].Situation > 0),
                () => Assert.False(updatedTrucks[3].Sct > 0),
                () => Assert.False(updatedTrucks[3].Insurance > 0),

            ]) ;
            #endregion
        }
        #endregion
    }
}