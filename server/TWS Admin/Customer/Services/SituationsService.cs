using Customer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services;
public class SituationsService : ISituationsService {

    readonly SituationsDepot Situations;

    public SituationsService(SituationsDepot situations) {
        this.Situations = situations;
    }

    public async Task<SetViewOut<Situation>> View(SetViewOptions options) {
        return await Situations.View(options);
    }

    public async Task<Situation> Create(Situation situation) {
        return await Situations.Create(situation);
    }
}
