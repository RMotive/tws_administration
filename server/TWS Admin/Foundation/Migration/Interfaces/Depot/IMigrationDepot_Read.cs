using System.Linq.Expressions;

using Foundation.Migration.Enumerators;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;

namespace Foundation.Migration.Interfaces.Depot;
public interface IMigrationDepot_Read<TMigrationSet>
    where TMigrationSet : IMigrationSet {

    public Task<MigrationTransactionResult<TMigrationSet>> Read(Expression<Func<TMigrationSet, bool>> Predicate, MigrationReadBehavior Behavior);
}
