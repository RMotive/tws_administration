using CSM_Foundation.Source.Models.Out;

namespace CSM_Foundation.Source.Interfaces.Depot;
public interface IMigrationDepot_Delete<TMigrationSet>
    where TMigrationSet : ISourceSet {

    public Task<SourceTransactionOut<TMigrationSet>> Delete(TMigrationSet[] migrations);

    public Task<TMigrationSet> Delete(TMigrationSet Set);
}
