using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Security.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface ISolutionsService {
    Task<SetViewOut<Solution>> View(SetViewOptions Options);
    Task<SourceTransactionOut<Solution>> Create(Solution[] Solutions);
    Task<RecordUpdateOut<Solution>> Update(Solution Solution);
}
