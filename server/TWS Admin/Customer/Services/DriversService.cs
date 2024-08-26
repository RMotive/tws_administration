

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;

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
        static IQueryable<Driver> include(IQueryable<Driver> query) {
            return query
            .Include(t => t.DriverCommonNavigation)
            .Include(t => t.EmployeeNavigation)
                .ThenInclude(i => i.IdentificationNavigation);

        }
        return await Drivers.View(Options, include);
    }
}
