
using Customer.Services.Interfaces;

using Foundation.Migrations.Records;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class SctService : ISctService {
    readonly SctDepot sctDepot;

    public SctService(SctDepot Solutions) {
        this.sctDepot = Solutions;
    }

    public async Task<MigrationView<Sct>> View(MigrationViewOptions options) {
        return await sctDepot.View(options, query => query
            .Include(m => m.Trucks)
            .Select(S => new Sct() {
                Id = S.Id,
                Type = S.Type,
                Number = S.Number,
                Configuration = S.Configuration,
                Trucks = (ICollection<Truck>)S.Trucks.Select(t => new Truck() {
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
