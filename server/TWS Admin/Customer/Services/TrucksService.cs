
using Customer.Services.Interfaces;
using Customer.Services.Records;
using Foundation.Migrations.Records;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class TrucksService: ITrucksService {

    readonly TruckDepot Trucks;

    public TrucksService(TruckDepot Trucks) {
          this.Trucks = Trucks;
    }

    public async Task<MigrationView<Truck>> View (MigrationViewOptions options) {
        return await Trucks.View (options);
    }


}
