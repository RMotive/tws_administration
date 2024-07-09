using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IMaintenancesService {

    Task<SetViewOut<Maintenance>> View(SetViewOptions Options);
}
