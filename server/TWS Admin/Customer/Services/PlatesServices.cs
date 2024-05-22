
using Customer.Services.Interfaces;

using Foundation.Migrations.Records;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class PlatesServices
    : IPlatesService {

    readonly PlatesDepot Plates;

    public PlatesServices(PlatesDepot plates) {
        this.Plates = plates;
    }

    public async Task<MigrationView<Plate>> View(MigrationViewOptions options) {
        return await Plates.View(options);
    }

    public async Task<Plate> Create(Plate plate) {
        return await Plates.Create(plate);
    }
}
