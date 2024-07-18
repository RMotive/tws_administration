using CSM_Foundation.Source.Models.Out;

namespace CSM_Foundation.Source.Interfaces.Depot;
public interface ISourceDepot_Delete<TSourceSet>
    where TSourceSet : ISourceSet {

    public Task<TSourceSet> Delete(int Id);

    public Task<SourceTransactionOut<TSourceSet>> Delete(TSourceSet[] migrations);

    public Task<TSourceSet> Delete(TSourceSet Set);
}
