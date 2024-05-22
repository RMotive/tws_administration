

using Foundation.Migrations.Records;

using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IMaintenancesService {

    Task<MigrationView<Maintenance>> View(MigrationViewOptions Options);
}
