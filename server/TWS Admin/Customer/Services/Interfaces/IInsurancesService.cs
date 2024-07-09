
using Foundation.Migrations.Records;

using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IInsurancesService {
    Task<MigrationView<Insurance>> View(MigrationViewOptions options);
}
