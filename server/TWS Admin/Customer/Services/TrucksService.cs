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

namespace TWS_Customer.Services;
public class TrucksService : ITrucksService {
    private readonly TruckDepot Trucks;
    private readonly InsurancesDepot Insurances;
    private readonly MaintenacesDepot Maintenaces;
    private readonly ManufacturersDepot Manufacturers;
    private readonly SctsDepot Sct;
    private readonly SituationsDepot Situations;
    private readonly PlatesDepot Plates;

    public TrucksService(
        TruckDepot Trucks, InsurancesDepot Insurances, MaintenacesDepot Maintenances,
        ManufacturersDepot Manufacturers, SctsDepot Sct, SituationsDepot Situations, PlatesDepot Plates) {
        this.Trucks = Trucks;
        this.Insurances = Insurances;
        Maintenaces = Maintenances;
        this.Manufacturers = Manufacturers;
        this.Sct = Sct;
        this.Situations = Situations;
        this.Plates = Plates;
    }

    public async Task<SetViewOut<Truck>> View(SetViewOptions options) {

        static IQueryable<Truck> include(IQueryable<Truck> query) {
            return query
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
        }

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
    private static async Task<int?> CreationHelper<T>(T? set, IMigrationDepot<T> depot, List<Lazy<Task>> nullifyCallback) where T : ISourceSet {
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
    public async Task<TruckAssembly> Create(TruckAssembly truck) {

        /// Stores the depot on success "Creation" inserts. If any error occurs, 
        /// this inserts will be removed using the "Delete" method. 
        List<Lazy<Task>> nullify = [];

        /// Stores the generated plates to remove on exception case.
        List<Plate> generatedPlates = [];

        /// Base model to generate a new truck.
        Truck assembly = new() {
            Vin = truck.Vin,
            Motor = truck.Motor,
        };

        /// Optional / Required validations.
        if (truck.Manufacturer == null) {
            throw new XTruckAssembly(XTruckAssemblySituation.RequiredManufacturer);
        }

        if (truck.Plates.IsNullOrEmpty()) {
            throw new XTruckAssembly(XTruckAssemblySituation.RequiredPlates);
        }

        try {
            /// Validate which Manufacturer value use to assign the manufacturer value to the truck.
            if (truck.Manufacturer.Id == 0) {
                assembly.Manufacturer = await CreationHelper(truck.Manufacturer, Manufacturers, nullify) ?? 0;
            } else {
                /// Pointer Validation
                SourceTransactionOut<Manufacturer> fetch = await Manufacturers.Read(i => i.Id == truck.Manufacturer.Id, MigrationReadBehavior.First);
                if (fetch.Failed) {
                    throw new XMigrationTransaction(fetch.Failures);
                }

                if (fetch.QTransactions == 0) {
                    throw new XTruckAssembly(XTruckAssemblySituation.ManufacturerNotExist);
                }

                assembly.Manufacturer = truck.Manufacturer.Id;
                truck.Manufacturer = fetch.Successes[0];
            }

            /// Validate which Situation value use to assign the manufacturer value to the truck.
            if (truck.Situation != null) {
                if (truck.Situation.Id == 0) {
                    assembly.Situation = await CreationHelper(truck.Situation, Situations, nullify) ?? 0;
                } else {
                    /// Pointer Validation
                    SourceTransactionOut<Situation> fetch = await Situations.Read(i => i.Id == truck.Situation.Id, MigrationReadBehavior.First);
                    if (fetch.Failed) {
                        throw new XMigrationTransaction(fetch.Failures);
                    }

                    if (fetch.QTransactions == 0) {
                        throw new XTruckAssembly(XTruckAssemblySituation.SitutionNotExist);
                    }

                    assembly.Situation = truck.Situation.Id;
                    truck.Situation = fetch.Successes[0];
                }
            }




            /// Create Optional fields bundle.
            assembly.Insurance = await CreationHelper(truck.Insurance, Insurances, nullify);
            assembly.Maintenance = await CreationHelper(truck.Maintenance, Maintenaces, nullify);
            assembly.Sct = await CreationHelper(truck.Sct, Sct, nullify);

            IMigrationDepot_Delete<Manufacturer> interfaceSol = Manufacturers;

            /// Create the defined truck.
            Truck result = await Trucks.Create(assembly);
            nullify.Add(new(() => Trucks.Delete(result)));

            /// validate and generate a plate list asocciated to this truck.
            if (!truck.Plates.IsNullOrEmpty()) {
                foreach (Plate plate in truck.Plates!) {
                    if (plate.Id == 0) {
                        plate.Id = 0;
                        plate.Truck = result.Id;
                        Plate currentPlate = await Plates.Create(plate);
                        generatedPlates.Add(currentPlate);
                    } else {
                        /// Update plate row.
                    }
                }
                truck.Plates = generatedPlates;
            }
            return truck;
        } catch (Exception) {
            // Undo all changes on data source
            /// Remove the last items to avoid key dependencies errors on data source.
            nullify.Reverse();
            foreach (Plate plate in generatedPlates) {
                _ = await Plates.Delete(plate);
            }

            foreach (Lazy<Task> disposition in nullify) {
                await disposition.Value;
            }

            throw;
        }
    }




}
