using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface ISituationsService {

    Task<SetViewOut<Situation>> View(SetViewOptions options);
    Task<Situation> Create(Situation situation);

}
