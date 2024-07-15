using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface IPlatesService {

    Task<SetViewOut<Plate>> View(SetViewOptions options);

    Task<Plate> Create(Plate plate);
}
