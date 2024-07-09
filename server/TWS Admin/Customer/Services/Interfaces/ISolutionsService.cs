using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Security.Sets;

namespace Customer.Services.Interfaces;
public interface ISolutionsService {
    Task<SetViewOut<Solution>> View(SetViewOptions Options);
    Task<SourceTransactionOut<Solution>> Create(Solution[] Solutions);
    Task<RecordUpdateOut<Solution>> Update(Solution Solution);
}
