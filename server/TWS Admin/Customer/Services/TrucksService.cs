
using System.Diagnostics;

using Customer.Services.Exceptions;
using Customer.Services.Interfaces;
using Customer.Services.Records;

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
    
    private static async Task<int?> CreationHelper<T>(dynamic? set, dynamic depot, List<(dynamic, dynamic)> nullifyList) where T : class {
        if (set != null) {
            set.Id = 0;
            dynamic result = await depot.Create(set);
            nullifyList.Add((depot, set));
            return result.Id;
        }
        return null;
    }
    public async Task<Truck> Create(TruckAssembly truck) {

        /// Stores the depot on success "Creation" inserts. If any error occurs, 
        /// this inserts will be removed using the "Delete" method. 
        List<(dynamic depot, dynamic set)> nullify = [];

        /// Stores the generated plates to remove on exception case.
        List<Plate> generatedPlates = [];

        /// Base model to generate a new truck.
        Truck assembly = new() {
            Vin = truck.Vin,
            Motor = truck.Motor,
        };

        /// Optional / Required validations.
        if (truck.Manufacturer == null && truck.ManufacturerPointer == null)
            throw new XTruckAssembly(XTrcukAssemblySituation.Required_Manufacturer);

        if (truck.Plates.IsNullOrEmpty() && truck.PlatePointer.IsNullOrEmpty())
            throw new XTruckAssembly(XTrcukAssemblySituation.Required_Plates);

        if (truck.Manufacturer != null && truck.ManufacturerPointer != null)
            throw new XTruckAssembly(XTrcukAssemblySituation.Multiple_Manufacturer_Input);

        if (truck.Plates != null && truck.PlatePointer != null)
            throw new XTruckAssembly(XTrcukAssemblySituation.Multiple_Plates_Input);

        try {
            /// Validate which Manufacturer value use to assign the manufacturer value to the truck.
            if (truck.Manufacturer != null) {
                assembly.Manufacturer = await CreationHelper<Manufacturer>(truck.Manufacturer, Manufacturers, nullify) ?? 0;
            } else if (truck.ManufacturerPointer != null) {
                /// use an existent insert.
                assembly.Manufacturer = (int)truck.ManufacturerPointer;
            }
            /// Create Optional fields bundle.
            assembly.Insurance = await CreationHelper<Insurance>(truck.Insurance, Insurances, nullify);
            assembly.Maintenance = await CreationHelper<Maintenance>(truck.Maintenance, Maintenaces, nullify);
            assembly.Sct = await CreationHelper<Sct>(truck.Sct, Sct, nullify);
            assembly.Situation = await CreationHelper<Situation>(truck.Situation, Situations, nullify);

            /// Create the defined truck.
            Truck result = await Trucks.Create(assembly);
            nullify.Add((Trucks, result));

            /// validate and generate a plate list asocciated to this truck.
            if (!truck.Plates.IsNullOrEmpty()) {
                foreach (Plate plate in truck.Plates!) {
                    plate.Id = 0;
                    plate.Truck = result.Id;
                    Plate currentPlate = await Plates.Create(plate);
                    generatedPlates.Add(currentPlate);
                }
                //assembly.Plates = generatedPlates;
            } else if (!truck.PlatePointer.IsNullOrEmpty()) {
                List<Plate> currentsPlates = [];
                foreach (int pointer in truck.PlatePointer!) {
                    ///update plate insert......
                }
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

            foreach ((dynamic depot, dynamic set) query in nullify)
                await query.depot.Delete(query.set);

            throw;
        }
    }




}
