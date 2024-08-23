using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="LoadTypesDepot"/>.
/// </summary>
public class Q_LoadTypesDepot
    : BQ_MigrationDepot<LoadType, LoadTypesDepot, TWSBusinessSource> {
    public Q_LoadTypesDepot()
        : base(nameof(LoadType.Id)) {
    }

    protected override LoadType MockFactory() {

        return new() {
            Name = RandomUtils.String(20),
        };
    }
}