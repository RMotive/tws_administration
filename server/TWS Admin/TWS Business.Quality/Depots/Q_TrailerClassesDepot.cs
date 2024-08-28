using CSM_Foundation.Core.Utils;
using CSM_Foundation.Source.Quality.Bases;

using TWS_Business.Depots;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Depots;
/// <summary>
///     Qualifies the <see cref="TrailerClassesDepot"/>.
/// </summary>
public class Q_TrailerClassesDepot
    : BQ_SourceDepot<TrailerClass, TrailerClassesDepot, TWSBusinessSource> {
    public Q_TrailerClassesDepot()
        : base(nameof(TrailerClass.Id)) {
    }

    protected override TrailerClass MockFactory() {

        return new() {
            Name = "Trailer Class name",
            Axis = 1
        };
    }
}