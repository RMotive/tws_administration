using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="SectionsDepot"/>.
/// </summary>
public class Q_SectionsDepot
    : BQ_MigrationDepot<Section, SectionsDepot, TWSBusinessSource> {
    public Q_SectionsDepot()
        : base(nameof(Section.Id)) {
    }

    protected override Section MockFactory() {

        return new() {
            Name = "Section A name",
            Status = 1,
            Yard = 1,
            Capacity = 20,
            Ocupancy = 10
        };
    }
}