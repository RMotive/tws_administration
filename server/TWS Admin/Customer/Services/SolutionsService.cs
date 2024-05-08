using Customer.Services.Interfaces;

using Foundation.Migrations.Records;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace Customer.Services;
public class SolutionsService 
    : ISolutionsService {
    readonly SolutionsDepot Solutions;

    public SolutionsService(SolutionsDepot Solutions) {
        this.Solutions = Solutions;
    }


    public async Task<MigrationView<Solution>> View(MigrationViewOptions Options) {
        return await Solutions.View(Options);
    }

    public async Task<MigrationTransactionResult<Solution>> Create(Solution[] Solutions) {
        return await this.Solutions.Create(Solutions);
    }
}
