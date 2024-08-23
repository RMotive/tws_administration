using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Approach : BQ_MigrationSet<Approach> {
    protected override Q_MigrationSet_EvaluateRecord<Approach>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Approach>[] Container) {

        Q_MigrationSet_EvaluateRecord<Approach> success = new() {
            Mock = new() {
                Id = 1,
                Email = "",
                Status = 1

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Approach> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Email = "",
                Status = 0
            },
            Expectations = [
                (nameof(Approach.Id), [(new PointerValidator(), 3)]),
                (nameof(Approach.Email), [(new LengthValidator(), 2)]),
                (nameof(Approach.Status), [(new PointerValidator(true), 3)]),

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}