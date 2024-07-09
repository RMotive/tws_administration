using CSMFoundation.Source.Models.In;
using CSMFoundation.Source.Models.Out;
using TWS_Business.Sets;

namespace Customer.Services.Interfaces;
public interface ISctService {
    Task<SetViewOut<Sct>> View(SetViewOptions Options);
}
