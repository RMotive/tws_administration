using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Carrier : BQ_MigrationSet<Carrier> {
    protected override Q_MigrationSet_EvaluateRecord<Carrier>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Carrier>[] Container) {
        PointerValidator pointer = new(true, false);

        Q_MigrationSet_EvaluateRecord<Carrier> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",
                Status = 1,
                Approach = 1,
                Address = 1
               
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Carrier> failAllCases = new() {
            Mock = new() {
                Id = -1,
                Name = "",
                Status = 0,
                Approach = 0,
                Address = 0
            },
            Expectations = [
                (nameof(Carrier.Id), [(new PointerValidator(), 3) ]),
                (nameof(Carrier.Name), [(new LengthValidator(),2)]),
                (nameof(Carrier.Approach), [(new PointerValidator(true), 3)]),
                (nameof(Carrier.Address), [(new PointerValidator(true), 3) ]),
                (nameof(Carrier.Status), [(new PointerValidator(true), 3) ])
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}