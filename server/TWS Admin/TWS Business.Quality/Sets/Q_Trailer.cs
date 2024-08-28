using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Trailer : BQ_MigrationSet<Trailer> {
    protected override Q_MigrationSet_EvaluateRecord<Trailer>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Trailer>[] Container) {

        Q_MigrationSet_EvaluateRecord<Trailer> success = new() {
            Mock = new() {
                Id = 1,
                Common = 0,
                Manufacturer = 0,
                Maintenance = 0,
                Status = 0
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Trailer> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Common = 0,
                Manufacturer = 0,
                Status = 0

            },
            Expectations = [
                (nameof(Trailer.Id), [(new PointerValidator(), 3)]),
                (nameof(Trailer.Common), [(new PointerValidator(true), 3)]),
                (nameof(Trailer.Manufacturer), [(new PointerValidator(true), 3)]),
                (nameof(Trailer.Status), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}