using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IManufacturersService {
    Task<SetViewOut<Manufacturer>> View(SetViewOptions Options);

    Task<Manufacturer> Create(Manufacturer manufacturer);
}
