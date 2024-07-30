using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Sets;

using TWS_Customer.Services.Records;

using TWS_Security.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface ITrucksService {
    Task<SetViewOut<Truck>> View(SetViewOptions options);
    Task<SourceTransactionOut<Truck>> Create(Truck[] trucks);
    Task<RecordUpdateOut<Truck>> Update(Truck Truck);


}
