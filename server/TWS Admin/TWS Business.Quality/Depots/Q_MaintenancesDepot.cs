using Foundation.Migrations.Quality.Bases;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;

/// <summary>
///     Qualifies the <see cref="MaintenacesDepot"/>.
/// </summary>
public class Q_MaintenancesDepot
    : BQ_MigrationDepot<Maintenance, MaintenacesDepot, TWSBusinessSource> {
    public Q_MaintenancesDepot()
        : base(nameof(Maintenance.Trimestral)) {
    }

    protected override Maintenance MockFactory() {
        DateOnly date = new(2024, 12, 12);

        return new() {
            Trimestral = date,
            Anual = date,
        };
    }
}