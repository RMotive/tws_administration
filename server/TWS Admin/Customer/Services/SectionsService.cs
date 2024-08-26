

using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.EntityFrameworkCore;

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
        static IQueryable<Section> include(IQueryable<Section> query) {
            return query
            .Include(t => t.LocationNavigation);
        }
        return await Sections.View(Options, include);
    }
}
