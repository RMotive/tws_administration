using Customer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services;
public class InsuranceService : IInsurancesService {
    readonly InsurancesDepot Insurances;

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
