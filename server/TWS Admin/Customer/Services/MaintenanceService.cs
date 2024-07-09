using Customer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services;
public class MaintenanceService : IMaintenancesService {
    readonly MaintenacesDepot Maintenances;

    public MaintenanceService(MaintenacesDepot Maintenances) {
        this.Maintenances = Maintenances;
    }

    public async Task<SetViewOut<Maintenance>> View(SetViewOptions Options) {
        return await Maintenances.View(Options, query => query
            .Include(m => m.Trucks)
            .Select(M => new Maintenance() {
                Id = M.Id,
                Anual = M.Anual,
                Trimestral = M.Trimestral,
                Trucks = (ICollection<Truck>)M.Trucks.Select(t => new Truck() {
                    Id = t.Id,
                    Vin = t.Vin,
                    Manufacturer = t.Manufacturer,
                    Motor = t.Motor,
                    Sct = t.Sct,
                    Maintenance = t.Maintenance,
                    Situation = t.Situation,
                    Insurance = t.Insurance,
                })
            }));
    }
}
