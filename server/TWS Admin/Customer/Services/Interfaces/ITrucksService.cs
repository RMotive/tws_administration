using Customer.Services.Records;

using CSMFoundation.Migration.Records;

using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface ITrucksService {
    Task<MigrationView<Truck>> View(MigrationViewOptions options);

    Task<TruckAssembly> Create(TruckAssembly truck);
}
