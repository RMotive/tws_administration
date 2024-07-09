using System.Linq.Expressions;

using CSMFoundation.Migration.Enumerators;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Source.Models.Out;

namespace CSMFoundation.Migration.Interfaces.Depot;
public interface IMigrationDepot_Read<TMigrationSet>
    where TMigrationSet : ISourceSet {

    public Task<SourceTransactionOut<TMigrationSet>> Read(Expression<Func<TMigrationSet, bool>> Predicate, MigrationReadBehavior Behavior, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? Incluide = null);
}
