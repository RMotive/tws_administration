

using CSMFoundation.Migration.Records;

using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface ISctService {
    Task<MigrationView<Sct>> View(MigrationViewOptions Options);
}
