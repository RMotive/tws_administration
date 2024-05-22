
using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class SctService : ISctService{
    readonly SctDepot sctDepot;

    public SctService(SctDepot Solutions) {
        this.sctDepot = Solutions;
    }

    public async Task<MigrationView<Sct>> View(MigrationViewOptions options) {
        return await sctDepot.View(options);
    }
}
