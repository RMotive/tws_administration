
using Customer.Services.Records;
using Foundation.Migrations.Records;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface ITrucksService {
    Task<MigrationView<Truck>> View(MigrationViewOptions options);

}
