using System.Linq.Expressions;

using CSMFoundation.Migration.Enumerators;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Migration.Records;

namespace CSMFoundation.Migration.Interfaces.Depot;
public interface IMigrationDepot_Read<TMigrationSet>
    where TMigrationSet : IMigrationSet {

    public Task<MigrationTransactionResult<TMigrationSet>> Read(Expression<Func<TMigrationSet, bool>> Predicate, MigrationReadBehavior Behavior, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? Incluide = null);
}
