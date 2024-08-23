

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class TrailersExternalsService : ITrailersExternalsService {
    private readonly TrailersExternalsDepot TrailersExternals;

    public TrailersExternalsService(TrailersExternalsDepot trailersExternals) {
        TrailersExternals = trailersExternals;
    }

    public async Task<SetViewOut<TrailerExternal>> View(SetViewOptions Options) {
        return await TrailersExternals.View(Options);
    }
}
