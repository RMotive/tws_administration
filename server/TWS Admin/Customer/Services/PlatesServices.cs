
using Customer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Depots;
using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;

namespace Customer.Services;
public class PlatesServices
    : IPlatesService {

    readonly PlatesDepot Plates;

    public PlatesServices(PlatesDepot plates) {
        this.Plates = plates;
    }

    public async Task<SetViewOut<Plate>> View(SetViewOptions options) {
        return await Plates.View(options, query => query
            .Include(p => p.TruckNavigation)
            .Select(p => new Plate() {
               Id = p.Id,
               Identifier = p.Identifier,
               State = p.State,
               Country = p.Country,
               Expiration = p.Expiration,
               Truck = p.Truck,
               TruckNavigation = p.TruckNavigation == null ? null : new Truck() {
                   Id = p.TruckNavigation.Id,
                   Vin = p.TruckNavigation.Vin,
                   Manufacturer = p.TruckNavigation.Manufacturer,
                   Motor = p.TruckNavigation.Motor,
                   Sct = p.TruckNavigation.Sct,
                   Maintenance = p.TruckNavigation.Maintenance,
                   Situation = p.TruckNavigation.Situation,
                   Insurance = p.TruckNavigation.Insurance,
               },

            }));
    }

    public async Task<Plate> Create(Plate plate) {
        return await Plates.Create(plate);
    }
}
