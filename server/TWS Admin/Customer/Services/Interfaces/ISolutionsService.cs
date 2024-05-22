using Foundation.Migrations.Records;

using TWS_Security.Sets;

namespace Customer.Services.Interfaces;
public interface ISolutionsService {
    Task<MigrationView<Solution>> View(MigrationViewOptions Options);
    Task<MigrationTransactionResult<Solution>> Create(Solution[] Solutions);
}
