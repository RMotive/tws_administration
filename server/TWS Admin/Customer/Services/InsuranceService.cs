using Customer.Services.Interfaces;

using Foundation.Migrations.Records;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class InsuranceService : IInsurancesService {
    readonly InsurancesDepot Insurances;

    public InsuranceService(InsurancesDepot Insurances) {
        this.Insurances = Insurances;
    }
    public async Task<MigrationView<Insurance>> View(MigrationViewOptions options) {
        
        return await Insurances.View(options, query => query
            .Include(m => m.Trucks)
            .Select(I => new Insurance() {
                Id = I.Id,
                Policy = I.Policy,
                Expiration = I.Expiration,
                Country = I.Country,
                Trucks = I.Trucks == null ? null : (ICollection<Truck>)I.Trucks.Select(t => new Truck() {
                    Id = t.Id,
                    Vin = t.Vin,
                    Manufacturer = t.Manufacturer,
                    Motor = t.Motor,
                    Sct = t.Sct,
                    Maintenance = t.Maintenance,
                    Situation = t.Situation,
                    Insurance = t.Insurance,
                })
            })
        );
    }
}
