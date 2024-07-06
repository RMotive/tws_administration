
using CSMFoundation.Migration.Records;

using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface IManufacturersService {
    Task<MigrationView<Manufacturer>> View(MigrationViewOptions Options);

    Task<Manufacturer> Create(Manufacturer manufacturer);
}
