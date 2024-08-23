

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Business.Depots;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace TWS_Customer.Services;
public class SectionsService : ISectionsService {
    private readonly SectionsDepot Sections;

    public SectionsService(SectionsDepot sections) {
        Sections = sections;
    }

    public async Task<SetViewOut<Section>> View(SetViewOptions Options) {
        return await Sections.View(Options);
    }
}
