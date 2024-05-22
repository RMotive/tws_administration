

using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;

namespace Foundation.Migration.Interfaces.Depot;
internal interface IMigrationDepot_Delete<TMigrationSet>
    where TMigrationSet : IMigrationSet {

    public Task<MigrationTransactionResult<TMigrationSet>> Delete(TMigrationSet[] migrations);

    public Task<TMigrationSet> Delete(TMigrationSet Set);
}
