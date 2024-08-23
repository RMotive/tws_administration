using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface ITrucksExternalsService {
    Task<SetViewOut<TruckExternal>> View(SetViewOptions Options);
}
