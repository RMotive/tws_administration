
using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using Foundation.Server.Interfaces;
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

    public async Task<Truck> Assembly(TruckAssembly truck) {
        Truck assembly = new() {
            Vin = truck.Vin,
            Motor = truck.Motor,
        };
        if (truck.Manufacturer == null && truck.ManufacturerPointer == null) {
            /// EXCEPTION......
        }

        if (truck.Plates.IsNullOrEmpty() && truck.PlatePointer.IsNullOrEmpty()) {
            /// EXCEPTION......
        }

        /// Validate which Manufacturer value use to assign the manufacturer value to the truck.
        if (truck.Manufacturer != null) {
            /// generate a new insert.
            Manufacturer ManufacturerResult = await Manufacturers.Create(truck.Manufacturer);
            assembly.Manufacturer = ManufacturerResult.Id;
        } else if (truck.ManufacturerPointer != null) {
            /// use an existent insert.
            assembly.Manufacturer = (int)truck.ManufacturerPointer;
        }

        if (truck.Insurance != null) {
            try {
                Insurance InsuranceResult = await Insurances.Create(truck.Insurance);
                assembly.Insurance = InsuranceResult.Id;
            } catch (Exception ex) when (ex is IServerTransactionException Exception) {

            }

        }
        if (truck.Maintenance != null) {
            Maintenance MaintenenceResult = await Maintenaces.Create(truck.Maintenance);
            assembly.Maintenance = MaintenenceResult.Id;
        }
        if (truck.Sct != null) {
            Sct SctResult = await Sct.Create(truck.Sct);
            assembly.Sct = SctResult.Id;
        }
        if (truck.Situation != null) {
            Situation SituationResult = await Situations.Create(truck.Situation);
            assembly.Situation = SituationResult.Id;
        }


        Truck result = await Trucks.Create(assembly);

        ///// validate and generate a plate list asocciated to this truck.
        //if (!truck.Plates.IsNullOrEmpty()) {
        //    List<Plate> currentsPlates = new List<Plate>();
        //    foreach (Plate plate in truck.Plates) {
        //        plate.Truck = result.Id;
        //        plate.TruckNavigation = result;
        //        currentsPlates.Add(await Plates.Create(plate));
        //    }
        //    assembly.Plates = currentsPlates;
        //} else if (!truck.PlatePointer.IsNullOrEmpty()) {
        //    List<Plate> currentsPlates = new List<Plate>();
        //    foreach (int pointer in truck.PlatePointer) {
        //        ///update plate insert......
        //    }
        //}

        return result;
    }


}
