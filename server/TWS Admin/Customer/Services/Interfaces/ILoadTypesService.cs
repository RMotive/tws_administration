using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface ILoadTypesService {
    Task<SetViewOut<LoadType>> View(SetViewOptions Options);
}
