

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class DriversService : IDriversService {
    private readonly DriversDepot Drivers;

    public DriversService(DriversDepot drivers) {
        Drivers = drivers;
    }

    public async Task<SetViewOut<Driver>> View(SetViewOptions Options) {
        return await Drivers.View(Options);
    }
}
