

using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class ManufacturersService : IManufacturersService {
    readonly ManufacturersDepot Manufacturers;
       
    public ManufacturersService(ManufacturersDepot manufacturers) {
        this.Manufacturers = manufacturers;
    }

    public async Task<MigrationView<Manufacturer>> View(MigrationViewOptions Options) {
       return await Manufacturers.View(Options);
    }
}
