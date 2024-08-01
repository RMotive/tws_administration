using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_HPTruck: BQ_MigrationSet<HPTruck> {
    protected override Q_MigrationSet_EvaluateRecord<HPTruck>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<HPTruck>[] Container) {

        Q_MigrationSet_EvaluateRecord<HPTruck> success = new() {
            Mock = new() {
                Id = 1,
                Creation = DateTime.Now,
                Vin = "",
                Motor = ""

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<HPTruck> failAllCases = new() {
            Mock = new() {
                Id = -1,
                Creation = DateTime.Now,
                Status = -1,
                Vin = "",
                Motor = ""
            },
            Expectations = [
                (nameof(HPTruck.Id), [(new PointerValidator(), 3) ]),
                (nameof(HPTruck.Vin), [(new LengthValidator(),2)]),
                (nameof(HPTruck.Motor), [(new LengthValidator(),2)]),
                (nameof(HPTruck.Status), [(new PointerValidator(isDependency: true),3)]),

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}