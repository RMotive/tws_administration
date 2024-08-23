using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface IYardLogsService {
    Task<SetViewOut<YardLog>> View(SetViewOptions options);
    Task<SourceTransactionOut<YardLog>> Create(YardLog[] trucks);
    Task<RecordUpdateOut<YardLog>> Update(YardLog YardLog, bool updatePivot);

}
