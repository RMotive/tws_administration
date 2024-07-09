

using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Source.Models.Out;

namespace CSMFoundation.Migration.Interfaces.Depot;
public interface IMigrationDepot_Delete<TMigrationSet>
    where TMigrationSet : ISourceSet {

    public Task<SourceTransactionOut<TMigrationSet>> Delete(TMigrationSet[] migrations);

    public Task<TMigrationSet> Delete(TMigrationSet Set);
}
