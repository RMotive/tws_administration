

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class TrucksExternalsService : ITrucksExternalsService {
    private readonly TrucksExternalsDepot TrucksExternals;

    public TrucksExternalsService(TrucksExternalsDepot trucksExternals) {
        TrucksExternals = trucksExternals;
    }

    public async Task<SetViewOut<TruckExternal>> View(SetViewOptions Options) {
        return await TrucksExternals.View(Options);
    }
}
