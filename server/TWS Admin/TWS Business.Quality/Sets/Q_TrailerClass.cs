using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_TrailerClass : BQ_MigrationSet<TrailerClass> {
    protected override Q_MigrationSet_EvaluateRecord<TrailerClass>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<TrailerClass>[] Container) {

        Q_MigrationSet_EvaluateRecord<TrailerClass> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",
                Axis = 1
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<TrailerClass> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Name = "",
                Axis = 0

            },
            Expectations = [
                (nameof(TrailerClass.Id), [(new PointerValidator(), 3)]),
                (nameof(TrailerClass.Name), [(new LengthValidator(), 2)]),
                (nameof(TrailerClass.Axis), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}