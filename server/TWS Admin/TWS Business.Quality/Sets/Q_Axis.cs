using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Axis : BQ_MigrationSet<Axis> {
    protected override Q_MigrationSet_EvaluateRecord<Axis>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Axis>[] Container) {

        Q_MigrationSet_EvaluateRecord<Axis> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",
                Quantity = 1,

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Axis> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Name = ""
            },
            Expectations = [
                (nameof(Axis.Id), [(new PointerValidator(), 3)]),
                (nameof(Axis.Name), [(new LengthValidator(), 2)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}