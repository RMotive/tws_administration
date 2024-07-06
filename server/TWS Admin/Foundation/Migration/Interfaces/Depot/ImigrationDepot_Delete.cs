

using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Records;

namespace CSMFoundation.Migration.Interfaces.Depot;
public interface IMigrationDepot_Delete<TMigrationSet>
    where TMigrationSet : IMigrationSet {

    public Task<MigrationTransactionResult<TMigrationSet>> Delete(TMigrationSet[] migrations);

    public Task<TMigrationSet> Delete(TMigrationSet Set);
}
