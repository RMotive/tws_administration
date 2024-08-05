using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Sets;

namespace TWS_Customer.Services.Interfaces;
public interface IHPTruckService {

    Task<SetViewOut<HPTruck>> View(SetViewOptions options);
    Task<SourceTransactionOut<HPTruck>> Create(HPTruck[] trucks);
    Task<RecordUpdateOut<HPTruck>> Update(HPTruck Truck);


}
