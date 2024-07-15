
using System.Diagnostics;

using Customer.Services.Exceptions;
using Customer.Services.Interfaces;
using Customer.Services.Records;
using Customer.Shared.Exceptions;

using Foundation.Migration.Enumerators;
using Foundation.Migration.Interfaces.Depot;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class TrucksService : ITrucksService {

    readonly TruckDepot Trucks;
    readonly InsurancesDepot Insurances;
    readonly MaintenacesDepot Maintenaces;
    readonly ManufacturersDepot Manufacturers;
    readonly SctsDepot Sct;
    readonly SituationsDepot Situations;
    readonly PlatesDepot Plates;

    public TrucksService(
        TruckDepot Trucks, InsurancesDepot Insurances, MaintenacesDepot Maintenances,
        ManufacturersDepot Manufacturers, SctsDepot Sct, SituationsDepot Situations, PlatesDepot Plates) {
        this.Trucks = Trucks;
        this.Insurances = Insurances;
        this.Maintenaces = Maintenances;
        this.Manufacturers = Manufacturers;
        this.Sct = Sct;
        this.Situations = Situations;
        this.Plates = Plates;
    }

    public async Task<MigrationView<Truck>> View(MigrationViewOptions options) {

        Func<IQueryable<Truck>, IQueryable<Truck>> include =
            query => query
            .Include(t => t.InsuranceNavigation)
            .Include(t => t.ManufacturerNavigation)
            .Include(t => t.MaintenanceNavigation)
            .Include(t => t.SctNavigation)
            .Include(t => t.SituationNavigation)
            .Include(t => t.Plates)
            .Select(t => new Truck() {
                Id = t.Id,
                Vin = t.Vin,
                Manufacturer = t.Manufacturer,
                Motor = t.Motor,
                Sct = t.Sct,
                Maintenance = t.Maintenance,
                Situation = t.Situation,
                Insurance = t.Insurance,
                SctNavigation = t.SctNavigation == null ? null : new Sct() {
                    Id = t.SctNavigation.Id,
                    Type = t.SctNavigation.Type,
                    Number = t.SctNavigation.Number,
                    Configuration = t.SctNavigation.Configuration
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

        return await Trucks.View(options, include);
    }

    /// <summary>
    /// Method that verify the data input and generate a new set insert based on the parameters.
    /// </summary>
    /// <typeparam name="T">
    /// Set class to generate.
    /// </typeparam>
    /// <param name="set">
    /// Set object.
    /// </param>
    /// <param name="depot">
    /// Depot to insert the given set.
    /// </param>
    /// <param name="nullifyList">
    /// Current acumulator list that stores the already generated sets/inserts.
    /// </param>
    /// <returns></returns>
    private static async Task<int?> CreationHelper<T>(T? set, IMigrationDepot<T> depot, List<Lazy<Task>> nullifyCallback, T? navigation) where T : IMigrationSet {
        if (set != null) {
            set.Id = 0;
            T result = await depot.Create(set);
            set = result;
            //navigation = result;
            nullifyCallback.Add(new(() => depot.Delete(result)));
            return result.Id;
        }
        return null;
    }
    public async Task<MigrationTransactionResult<Truck>> Create(Truck[] trucks) {
        return await this.Trucks.Create(trucks);

    }




}
