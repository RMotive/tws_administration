using Foundation.Migrations.Records;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public  interface IPlatesService {

    Task<MigrationView<Plate>> View(MigrationViewOptions options);
}
