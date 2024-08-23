using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Driver : BQ_MigrationSet<Driver> {
    protected override Q_MigrationSet_EvaluateRecord<Driver>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Driver>[] Container) {

        Q_MigrationSet_EvaluateRecord<Driver> success = new() {
            Mock = new() {
                Id = 1,
                Status = 0,
                Employee = 0,
                DriverType = "",
                Common = 0
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Driver> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Status = 0,
                Employee = 0,
                DriverType = "",
                Common = 0
            },
            Expectations = [
                (nameof(Driver.Id), [(new PointerValidator(), 3)]),
                (nameof(Driver.DriverType), [(new LengthValidator(), 2)]),
                (nameof(Driver.Employee), [(new PointerValidator(true), 3)]),
                (nameof(Driver.Common), [(new PointerValidator(true), 3)]),
                (nameof(Driver.Status), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}