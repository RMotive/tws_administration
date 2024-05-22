

using Foundation.Migrations.Records;

using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface ISituationsService {

    Task<MigrationView<Situation>> View(MigrationViewOptions options);
}
