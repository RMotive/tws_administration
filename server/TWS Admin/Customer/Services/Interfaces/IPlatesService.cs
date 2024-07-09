using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IPlatesService {

    Task<SetViewOut<Plate>> View(SetViewOptions options);

    Task<Plate> Create(Plate plate);
}
