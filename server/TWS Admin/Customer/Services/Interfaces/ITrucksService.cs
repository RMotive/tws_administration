using Customer.Services.Records;

using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services.Interfaces;
public interface ITrucksService {
    Task<SetViewOut<Truck>> View(SetViewOptions options);

    Task<TruckAssembly> Create(TruckAssembly truck);
}
