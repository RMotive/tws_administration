using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="SctsDepot"/>.
/// </summary>
public class Q_SctsDepot
    : BQ_SourceDepot<Sct, SctsDepot, TWSBusinessSource> {
    public Q_SctsDepot()
        : base(nameof(Sct.Type)) {
    }

    protected override Sct MockFactory() {
        return new() {
            Type = RandomUtils.String(6),
            Number = RandomUtils.String(25),
            Configuration = RandomUtils.String(10)
        };
    }
}