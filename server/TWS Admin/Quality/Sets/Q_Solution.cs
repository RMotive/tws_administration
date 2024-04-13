using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using TWS_Security.Sets;


namespace TWS_Security.Quality.Sets;
public class Q_Solution
    : BQ_MigrationSet<Solution> {
    protected override Q_MigrationSet_EvaluateRecord<Solution>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Solution>[] Container) {
        
        Q_MigrationSet_EvaluateRecord<Solution> Success = new() {
            Mock = new() {
                Name = "Doscientos ciencuenta y ocho",
            },
            Expectations = [],
        };

        Container = [..Container, Success ];


        return Container;
    }
}
