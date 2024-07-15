using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Situation : BQ_MigrationSet<Situation> {
    protected override Q_MigrationSet_EvaluateRecord<Situation>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Situation>[] Container) {

        Q_MigrationSet_EvaluateRecord<Situation> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Situation> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Name = "Situation validation test, max lengh 25 characters",
                Description = ""
            },
            Expectations = [
                (nameof(Situation.Id), [(new PointerValidator(), 3)]),
                (nameof(Situation.Name), [(new LengthValidator(), 3)]),
                (nameof(Situation.Description), [(new LengthValidator(), 2)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}
