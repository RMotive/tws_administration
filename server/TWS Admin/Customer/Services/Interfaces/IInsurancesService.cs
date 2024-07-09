using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IInsurancesService {
    Task<SetViewOut<Insurance>> View(SetViewOptions options);
}
