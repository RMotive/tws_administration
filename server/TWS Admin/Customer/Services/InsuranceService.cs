using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class InsuranceService : IInsurancesService {
    private readonly InsurancesDepot Insurances;

    public InsuranceService(InsurancesDepot Insurances) {
        this.Insurances = Insurances;
    }
    public async Task<SetViewOut<Insurance>> View(SetViewOptions options) {

        return await Insurances.View(options, query => query
            .Include(m => m.Trucks)
            .Select(I => new Insurance() {
                Id = I.Id,
                Policy = I.Policy,
                Expiration = I.Expiration,
                Country = I.Country,
                Trucks = (ICollection<Truck>)I.Trucks.Select(t => new Truck() {
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
