using CSM_Foundation.Source.Enumerators;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Interfaces.Depot;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Core.Exceptions;
using TWS_Customer.Services.Exceptions;
using TWS_Customer.Services.Interfaces;
using TWS_Customer.Services.Records;

using TWS_Security.Depots;
using TWS_Security.Sets;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TWS_Customer.Services;
public class HPTrucksService : IHPTruckService {
    private readonly TruckDepot Trucks;
    private readonly InsurancesDepot Insurances;
    private readonly MaintenacesDepot Maintenaces;
    private readonly ManufacturersDepot Manufacturers;
    private readonly StatusesDepot Statuses;
    private readonly HP_TruckDepot HP_Truck;
    private readonly SctsDepot Sct;
    private readonly SituationsDepot Situations;
    private readonly PlatesDepot Plates;

    public HPTrucksService(
        TruckDepot Trucks, InsurancesDepot Insurances, MaintenacesDepot Maintenances,
        ManufacturersDepot Manufacturers, SctsDepot Scts, SituationsDepot Situations, PlatesDepot Plates, StatusesDepot statusesDepot, HP_TruckDepot HP_TrucksDepot) {
        this.Trucks = Trucks;
        this.Insurances = Insurances;
        this.Maintenaces = Maintenances;
        this.Manufacturers = Manufacturers;
        this.Sct = Scts;
        this.Situations = Situations;
        this.Plates = Plates;
        this.Statuses = statusesDepot;
        this.HP_Truck = HP_TrucksDepot;
    }

    public async Task<SetViewOut<HPTruck>> View(SetViewOptions options) {

        static IQueryable<HPTruck> include(IQueryable<HPTruck> query) {
            return query
                .Include(x => x.Trucks)
                .Include(x => x.StatusNavigation)
                .Select(t => new HPTruck() {
                    Id = t.Id,
                    Vin = t.Vin,
                    Motor = t.Motor,
                    Creation = t.Creation,
                    Status = t.Status,
                    Trucks = (ICollection<Truck>)t.Trucks.Select(p => new Truck() {
                        Id = p.Id,
                        Vin = p.Vin,
                        Motor = p.Motor,
                        Manufacturer = p.Manufacturer,
                        Hp = p.Hp,
                        Modified = p.Modified,
                        Sct = p.Sct,
                        Maintenance = p.Maintenance,
                        Situation = p.Situation,
                        Insurance = p.Insurance,
                        InsuranceNavigation = p.InsuranceNavigation,
                        MaintenanceNavigation = p.MaintenanceNavigation,
                        ManufacturerNavigation = p.ManufacturerNavigation,
                        SctNavigation = p.SctNavigation,
                        SituationNavigation = p.SituationNavigation,
                        Plates = (ICollection<Plate>)p.Plates.Select(p => new Plate() {
                            Id = p.Id,
                            Identifier = p.Identifier,
                            State = p.State,
                            Country = p.Country,
                            Expiration = p.Expiration,
                            Truck = p.Truck
                        })
                    }),
                    StatusNavigation = t.StatusNavigation,

                }); 
        }

        return await HP_Truck.View(options, include);
    }

    public async Task<SourceTransactionOut<HPTruck>> Create(HPTruck[] HPTrucks) {
        return await this.HP_Truck.Create(HPTrucks);
    }
    public async Task<RecordUpdateOut<HPTruck>> Update(HPTruck Truck) {

        static HPTruck GetPivot(Truck truck) {
            return truck.HPNavigation!;
        }

        static IQueryable<HPTruck> include(IQueryable<HPTruck> query) {
            return query
            .Include(t => t.StatusNavigation)
            .Include(t => t.Trucks).ThenInclude(t => t.Plates)
            .Include(t => t.Trucks).ThenInclude(t => t.InsuranceNavigation)
            .Include(t => t.Trucks).ThenInclude(t => t.MaintenanceNavigation)
            .Include(t => t.Trucks).ThenInclude(t => t.ManufacturerNavigation)
            .Include(t => t.Trucks).ThenInclude(t => t.SctNavigation)
            .Include(t => t.Trucks).ThenInclude(t => t.SituationNavigation);
        }       


        return await HP_Truck.Update(Truck, include);


    }



}
