

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class DriversExternalsService : IDriversExternalsService {
    private readonly DriversExternalsDepot DriversExternals;

    public DriversExternalsService(DriversExternalsDepot driversExternals) {
        DriversExternals = driversExternals;
    }

    public async Task<SetViewOut<DriverExternal>> View(SetViewOptions Options) {
        return await DriversExternals.View(Options);
    }
}
