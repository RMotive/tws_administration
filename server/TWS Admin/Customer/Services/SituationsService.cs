using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class SituationsService : ISituationsService {
    private readonly SituationsDepot Situations;

    public SituationsService(SituationsDepot situations) {
        Situations = situations;
    }

    public async Task<SetViewOut<Situation>> View(SetViewOptions options) {
        return await Situations.View(options);
    }

    public async Task<Situation> Create(Situation situation) {
        return await Situations.Create(situation);
    }
}
