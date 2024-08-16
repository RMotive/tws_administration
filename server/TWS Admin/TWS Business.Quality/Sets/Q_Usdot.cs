using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Usdot : BQ_MigrationSet<Usdot> {
    protected override Q_MigrationSet_EvaluateRecord<Usdot>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Usdot>[] Container) {
        PointerValidator pointer = new(true, false);

        Q_MigrationSet_EvaluateRecord<Usdot> success = new() {
            Mock = new() {
                Id = 1,
                Status = 1,
                Mc = "",
                Scac = ""
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Usdot> failAllCases = new() {
            Mock = new() {
                Id = -1,
                Status = -1,
                Mc = "",
                Scac = ""
            },
            Expectations = [
                (nameof(Usdot.Id), [(new PointerValidator(), 3) ]),
                (nameof(Usdot.Mc), [(new LengthValidator(),2)]),
                (nameof(Usdot.Scac), [(new LengthValidator(), 2)]),
                (nameof(Usdot.Status), [(new PointerValidator(), 3) ])
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}