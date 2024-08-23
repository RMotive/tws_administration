

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class TrailersService : ITrailersService {
    private readonly TrailersDepot Trailers;

    public TrailersService(TrailersDepot trailers) {
        Trailers = trailers;
    }

    public async Task<SetViewOut<Trailer>> View(SetViewOptions Options) {
        return await Trailers.View(Options);
    }
}
