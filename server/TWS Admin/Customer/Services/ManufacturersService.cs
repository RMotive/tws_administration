

using Customer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services;
public class ManufacturersService : IManufacturersService {
    readonly ManufacturersDepot Manufacturers;

    public ManufacturersService(ManufacturersDepot manufacturers) {
        this.Manufacturers = manufacturers;
    }

    public async Task<SetViewOut<Manufacturer>> View(SetViewOptions Options) {
        return await Manufacturers.View(Options);
    }

    public async Task<Manufacturer> Create(Manufacturer manufacturer) {
        return await Manufacturers.Create(manufacturer);
    }
}
