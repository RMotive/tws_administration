

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class LoadTypesService : ILoadTypesService {
    private readonly LoadTypesDepot LoadTypes;

    public LoadTypesService(LoadTypesDepot loadTypes) {
        LoadTypes = loadTypes;
    }

    public async Task<SetViewOut<LoadType>> View(SetViewOptions Options) {
        return await LoadTypes.View(Options);
    }
}
