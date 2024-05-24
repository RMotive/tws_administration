
using System.Diagnostics;

using Customer.Services.Exceptions;
using Customer.Services.Interfaces;
using Customer.Services.Records;
using Customer.Shared.Exceptions;

using Foundation.Migration.Enumerators;
using Foundation.Migration.Interfaces.Depot;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;

using Microsoft.IdentityModel.Tokens;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class TrucksService : ITrucksService {

    readonly TruckDepot Trucks;
    readonly InsurancesDepot Insurances;
    readonly MaintenaceDepot Maintenaces;
    readonly ManufacturersDepot Manufacturers;
    readonly SctDepot Sct;
    readonly SituationsDepot Situations;
    readonly PlatesDepot Plates;

    public TrucksService(
        TruckDepot Trucks, InsurancesDepot Insurances, MaintenaceDepot Maintenances,
        ManufacturersDepot Manufacturers, SctDepot Sct, SituationsDepot Situations, PlatesDepot Plates) {
        this.Trucks = Trucks;
        this.Insurances = Insurances;
        this.Maintenaces = Maintenances;
        this.Manufacturers = Manufacturers;
        this.Sct = Sct;
        this.Situations = Situations;
        this.Plates = Plates;
    }

    public async Task<MigrationView<Truck>> View(MigrationViewOptions options) {
        return await Trucks.View(options);
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
    private static async Task<int?> CreationHelper<T>(T? set, IMigrationDepot<T> depot, List<(IMigrationDepot_Delete<IMigrationSet>, IMigrationSet)> nullifyCallback) where T : IMigrationSet {
        if (set != null) {
            set.Id = 0;
            T result = await depot.Create(set);
            nullifyCallback.Add(((IMigrationDepot_Delete<IMigrationSet>)depot, result));
            return result.Id;
        }
        return null;
    }
    public async Task<Truck> Create(TruckAssembly truck) {

        /// Stores the depot on success "Creation" inserts. If any error occurs, 
        /// this inserts will be removed using the "Delete" method. 
        List<(IMigrationDepot_Delete<IMigrationSet>, IMigrationSet)> nullify = [];

        /// Stores the generated plates to remove on exception case.
        List<Plate> generatedPlates = [];

        /// Base model to generate a new truck.
        Truck assembly = new() {
            Vin = truck.Vin,
            Motor = truck.Motor,
        };

        /// Optional / Required validations.
        if (truck.Manufacturer == null)
            throw new XTruckAssembly(XTrcukAssemblySituation.Required_Manufacturer);

        if (truck.Plates.IsNullOrEmpty())
            throw new XTruckAssembly(XTrcukAssemblySituation.Required_Plates);

        try {
            /// Validate which Manufacturer value use to assign the manufacturer value to the truck.
            if (truck.Manufacturer.Id == 0) {
                assembly.Manufacturer = await CreationHelper(truck.Manufacturer, Manufacturers, nullify) ?? 0;
            } else {
                /// Pointer Validation
                MigrationTransactionResult<Manufacturer> fetch = await Manufacturers.Read(i => i.Id == truck.Manufacturer.Id, MigrationReadBehavior.First);
                if (fetch.Failed)
                    throw new XMigrationTransaction(fetch.Failures);

                if (fetch.QTransactions == 0)
                    throw new XTruckAssembly(XTrcukAssemblySituation.Manufacturer_Not_Exist);

                assembly.Manufacturer = truck.Manufacturer.Id;
            }
            /// Create Optional fields bundle.
            assembly.Insurance = await CreationHelper(truck.Insurance, Insurances, nullify);
            assembly.Maintenance = await CreationHelper(truck.Maintenance, Maintenaces, nullify);
            assembly.Sct = await CreationHelper(truck.Sct, Sct, nullify);
            assembly.Situation = await CreationHelper(truck.Situation, Situations, nullify);

            IMigrationDepot_Delete<Manufacturer> interfaceSol = Manufacturers;

            /// Create the defined truck.
            Truck result = await Trucks.Create(assembly);
            nullify.Add(((IMigrationDepot_Delete<IMigrationSet>)Trucks, result));

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
                //assembly.Plates = generatedPlates;
            }
            return result;
        } catch (Exception ex) {
            // Undo all changes on data source.
            Debug.WriteLine("Ejecutando: ToString.....");
            Debug.WriteLine(ex.ToString());

            /// Remove the last items to avoid key dependencies errors on data source.
            nullify.Reverse();
            foreach (Plate plate in generatedPlates)
                await Plates.Delete(plate);

            foreach ((IMigrationDepot_Delete<IMigrationSet> depot, IMigrationSet set) query in nullify)
                await query.depot.Delete(query.set);

            throw;
        }
    }




}
