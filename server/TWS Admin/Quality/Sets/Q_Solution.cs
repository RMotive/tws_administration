using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;

using TWS_Security.Sets;


namespace TWS_Security.Quality.Sets;
public class Q_Solution
    : BQ_MigrationSet<Solution> {
    protected override Q_MigrationSet_EvaluateRecord<Solution>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Solution>[] Container) {

        Q_MigrationSet_EvaluateRecord<Solution> success = new() {
            Mock = new() {
                Id = 1,
                Name = "TWS Administration",
                Sign = "TWSMA",
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Solution> failByPointer = new() {
            Mock = new() {
                Id = 0,
                Name = "TWS Administration",
                Sign = "TWSMA",
            },
            Expectations = [
                (nameof(Solution.Id), [(new PointerValidator(), 1)]),
            ],
        };


        Container = [.. Container, success, failByPointer];


        return Container;
    }
}
