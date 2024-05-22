using Customer.Services.Interfaces;

using Foundation.Migrations.Records;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class MaintenanceService : IMaintenancesService {
    readonly MaintenaceDepot Maintenances;

    public MaintenanceService(MaintenaceDepot Maintenances) {
        this.Maintenances = Maintenances;
    }

    public async Task<MigrationView<Maintenance>> View(MigrationViewOptions Options) {
        return await Maintenances.View(Options);
    }
}
