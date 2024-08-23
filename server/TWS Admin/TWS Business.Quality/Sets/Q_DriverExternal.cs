using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_DriverExternal : BQ_MigrationSet<DriverExternal> {
    protected override Q_MigrationSet_EvaluateRecord<DriverExternal>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<DriverExternal>[] Container) {

        Q_MigrationSet_EvaluateRecord<DriverExternal> success = new() {
            Mock = new() {
                Id = 1,
                Status = 0,
                Common = 0,
                Identification = 0
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<DriverExternal> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Status = 0,
                Common = 0,
                Identification = 0
            },
            Expectations = [
                (nameof(DriverExternal.Id), [(new PointerValidator(), 3)]),
                (nameof(DriverExternal.Status), [(new PointerValidator(), 3)]),
                (nameof(DriverExternal.Common), [(new PointerValidator(), 3)]),
                (nameof(DriverExternal.Identification), [(new PointerValidator(), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}