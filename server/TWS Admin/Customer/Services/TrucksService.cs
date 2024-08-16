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
public class TrucksService : ITrucksService {
    private readonly TruckDepot Trucks;
    private readonly InsurancesDepot Insurances;
    private readonly MaintenacesDepot Maintenaces;
    private readonly ManufacturersDepot Manufacturers;
    private readonly StatusesDepot StatusesDepot;
    private readonly TrucksHDepot HP_TruckDepot;
    private readonly SctsDepot Sct;
    private readonly SituationsDepot Situations;
    private readonly PlatesDepot Plates;

    public TrucksService(
        TruckDepot Trucks, InsurancesDepot Insurances, MaintenacesDepot Maintenances,
        ManufacturersDepot Manufacturers, SctsDepot Scts, SituationsDepot Situations, PlatesDepot Plates, StatusesDepot statusesDepot, TrucksHDepot HP_TrucksDepot) {
        this.Trucks = Trucks;
        this.Insurances = Insurances;
        this.Maintenaces = Maintenances;
        this.Manufacturers = Manufacturers;
        this.Sct = Scts;
        this.Situations = Situations;
        this.Plates = Plates;
        this.StatusesDepot = statusesDepot;
        this.HP_TruckDepot = HP_TrucksDepot;
    }

    public async Task<SetViewOut<Truck>> View(SetViewOptions options) {

        static IQueryable<Truck> include(IQueryable<Truck> query) {
            return query
            .Include(t => t.InsuranceNavigation)
            .Include(t => t.ManufacturerNavigation)
            .Include(t => t.MaintenanceNavigation)
            .Include(t => t.CarrierNavigation)
            .Include(t => t.SituationNavigation)
            .Include(t => t.StatusNavigation)
            .Include(t => t.Plates)
            .Select(t => new Truck() {
                 Id = t.Id,
                 Vin = t.Vin,
                 Status = t.Status,
                 Manufacturer = t.Manufacturer,
                 Economic = t.Economic,
                 Motor = t.Motor,
                 Maintenance = t.Maintenance,
                 Situation = t.Situation,
                 Insurance = t.Insurance,
                 Carrier = t.Carrier,
                 StatusNavigation = t.StatusNavigation == null? null : new Status() {
                     Id = t.StatusNavigation.Id,
                     Name = t.StatusNavigation.Name,
                     Description = t.StatusNavigation.Description,
                 },
                 CarrierNavigation = t.CarrierNavigation == null? null : new Carrier() {
                     Id = t.CarrierNavigation.Id,
                     Status = t.CarrierNavigation.Status,
                     Name = t.CarrierNavigation.Name,
                     Contact = t.CarrierNavigation.Contact,
                     Address = t.CarrierNavigation.Address,
                     Usdot = t.CarrierNavigation.Usdot,
                     Sct = t.CarrierNavigation.Sct,
                     ContactNavigation = t.CarrierNavigation.ContactNavigation,
                     AddressNavigation = t.CarrierNavigation.AddressNavigation,
                     UsdotNavigation = t.CarrierNavigation.UsdotNavigation,
                     SctNavigation = t.CarrierNavigation.SctNavigation
                 },
                 MaintenanceNavigation = t.MaintenanceNavigation == null ? null : new Maintenance() {
                     Id = t.MaintenanceNavigation.Id,
                     Anual = t.MaintenanceNavigation.Anual,
                     Trimestral = t.MaintenanceNavigation.Trimestral
                 },
                 ManufacturerNavigation = t.ManufacturerNavigation == null ? null : new Manufacturer() {
                     Id = t.ManufacturerNavigation.Id,
                     Model = t.ManufacturerNavigation.Model,
                     Brand = t.ManufacturerNavigation.Brand,
                     Year = t.ManufacturerNavigation.Year
                 },
                 InsuranceNavigation = t.InsuranceNavigation == null ? null : new Insurance() {
                     Id = t.InsuranceNavigation.Id,
                     Policy = t.InsuranceNavigation.Policy,
                     Expiration = t.InsuranceNavigation.Expiration,
                     Country = t.InsuranceNavigation.Country
                 },
                 SituationNavigation = t.SituationNavigation == null ? null : new Situation() {
                     Id = t.SituationNavigation.Id,
                     Name = t.SituationNavigation.Name,
                     Description = t.SituationNavigation.Description
                 },
                 Plates = (ICollection<Plate>)t.Plates.Select(p => new Plate() {
                     Id = p.Id,
                     Identifier = p.Identifier,
                     State = p.State,
                     Country = p.Country,
                     Expiration = p.Expiration,
                     Truck = p.Truck
                 })
             });
        }

        return await Trucks.View(options, include);
    }

    public async Task<SourceTransactionOut<Truck>> Create(Truck[] trucks) {
        return await this.Trucks.Create(trucks);
    }
    public async Task<RecordUpdateOut<Truck>> Update(Truck Truck, bool updatePivot = false) {
        
        static IQueryable<Truck> include(IQueryable<Truck> query) {
            return query
            .Include(t => t.InsuranceNavigation)
            .Include(t => t.ManufacturerNavigation)
            .Include(t => t.MaintenanceNavigation)
            .Include(t => t.SituationNavigation)
            .Include(t => t.StatusNavigation)
            .Include(t => t.Plates)
            .Include(t => t.CarrierNavigation)
                .ThenInclude(c => c!.AddressNavigation)
            .Include(t => t.CarrierNavigation)
                .ThenInclude(c => c!.ContactNavigation)
            .Include(t => t.CarrierNavigation)
                .ThenInclude(c => c!.SctNavigation)
            .Include(t => t.CarrierNavigation)
                .ThenInclude(c => c!.StatusNavigation)
            .Include(t => t.CarrierNavigation)
                .ThenInclude(c => c!.UsdotNavigation).Select(t => new Truck() {
                    Id = t.Id,
                    Vin = t.Vin,
                    Status = t.Status,
                    Manufacturer = t.Manufacturer,
                    Economic = t.Economic,
                    Motor = t.Motor,
                    Maintenance = t.Maintenance,
                    Situation = t.Situation,
                    Insurance = t.Insurance,
                    Carrier = t.Carrier,
                    StatusNavigation = t.StatusNavigation == null ? null : new Status() {
                        Id = t.StatusNavigation.Id,
                        Name = t.StatusNavigation.Name,
                        Description = t.StatusNavigation.Description,
                    },
                    CarrierNavigation = t.CarrierNavigation == null ? null : new Carrier() {
                        Id = t.CarrierNavigation.Id,
                        Status = t.CarrierNavigation.Status,
                        Name = t.CarrierNavigation.Name,
                        Contact = t.CarrierNavigation.Contact,
                        Address = t.CarrierNavigation.Address,
                        Usdot = t.CarrierNavigation.Usdot,
                        Sct = t.CarrierNavigation.Sct,
                        ContactNavigation = t.CarrierNavigation.ContactNavigation,
                        AddressNavigation = t.CarrierNavigation.AddressNavigation,
                        UsdotNavigation = t.CarrierNavigation.UsdotNavigation,
                        SctNavigation = t.CarrierNavigation.SctNavigation
                    },
                    MaintenanceNavigation = t.MaintenanceNavigation == null ? null : new Maintenance() {
                        Id = t.MaintenanceNavigation.Id,
                        Status = t.MaintenanceNavigation.Status,
                        Anual = t.MaintenanceNavigation.Anual,
                        Trimestral = t.MaintenanceNavigation.Trimestral,
                    },
                    ManufacturerNavigation = t.ManufacturerNavigation == null ? null : new Manufacturer() {
                        Id = t.ManufacturerNavigation.Id,
                        Model = t.ManufacturerNavigation.Model,
                        Brand = t.ManufacturerNavigation.Brand,
                        Year = t.ManufacturerNavigation.Year,
                    },
                    InsuranceNavigation = t.InsuranceNavigation == null ? null : new Insurance() {
                        Id = t.InsuranceNavigation.Id,
                        Status = t.InsuranceNavigation.Status,
                        Policy = t.InsuranceNavigation.Policy,
                        Expiration = t.InsuranceNavigation.Expiration,
                        Country = t.InsuranceNavigation.Country
                    },
                    SituationNavigation = t.SituationNavigation == null ? null : new Situation() {
                        Id = t.SituationNavigation.Id,
                        Name = t.SituationNavigation.Name,
                        Description = t.SituationNavigation.Description
                    },
                    Plates = (ICollection<Plate>)t.Plates.Select(p => new Plate() {
                        Id = p.Id,
                        Status = p.Status,
                        Identifier = p.Identifier,
                        State = p.State,
                        Country = p.Country,
                        Expiration = p.Expiration,
                        Truck = p.Truck
                    })
                });
        }
        
        return await Trucks.Update(Truck, include) ;


    }



}
