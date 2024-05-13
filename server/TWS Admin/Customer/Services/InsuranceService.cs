using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class InsuranceService: IInsurancesService {
    readonly InsurancesDepot Insurances;
    
    public InsuranceService(InsurancesDepot Insurances) {
        this.Insurances = Insurances;
    }
    public async Task<MigrationView<Insurance>> View(MigrationViewOptions options) {
        return await Insurances.View(options);
    }
}
