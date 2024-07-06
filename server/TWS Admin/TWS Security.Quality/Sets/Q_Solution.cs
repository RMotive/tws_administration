using CSMFoundation.Migration.Quality.Bases;
using CSMFoundation.Migration.Quality.Records;
using CSMFoundation.Migration.Validators;

using TWS_Security.Sets;


namespace TWS_Security.Quality.Sets;
public class Q_Solution
    : BQ_MigrationSet<Solution> {
    protected override Q_MigrationSet_EvaluateRecord<Solution>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Solution>[] Container) {
        const string successName = "TWS Quality";
        const string successSign = "TWSMQ";

        Q_MigrationSet_EvaluateRecord<Solution> success = new() {
            Mock = new() {
                Id = 1,
                Name = successName,
                Sign = successSign,
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Solution> failure = new() {
            Mock = new() { },
            Expectations = [
                (nameof(Solution.Id), [(new PointerValidator(), 3)]),
                (nameof(Solution.Name), [(new LengthValidator(), 1)]),
                (nameof(Solution.Sign), [(new LengthValidator(), 1)]),
            ],
        };

        Container = [.. Container, success, failure];
        return Container;
    }
}
