using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
// <summary>
///     Qualifies the <see cref="PlatesDepot"/>.
/// </summary>
public class Q_PlatesDepot
    : BQ_MigrationDepot<Plate, PlatesDepot, TWSBusinessSource> {
    public Q_PlatesDepot()
        : base(nameof(Plate.Identifier)) {
    }

    protected override Plate MockFactory() {
        DateOnly date = new(2024, 12, 12);

        return new() {
            Identifier = RandomUtils.String(12),
            State = RandomUtils.String(3),
            Country = RandomUtils.String(3),
            Expiration = date,
            Truck = 3,
            Status = 1
        };
    }
}