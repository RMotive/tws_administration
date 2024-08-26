using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_DriverCommon : BQ_MigrationSet<DriverCommon> {
    protected override Q_MigrationSet_EvaluateRecord<DriverCommon>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<DriverCommon>[] Container) {

        Q_MigrationSet_EvaluateRecord<DriverCommon> success = new() {
            Mock = new() {
                Id = 1,
                Situation = 0,
                License = ""

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<DriverCommon> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Situation = 0,
                License = "",
                Status = 0
            },
            Expectations = [
                (nameof(DriverCommon.Id), [(new PointerValidator(), 3)]),
                (nameof(DriverCommon.License), [(new LengthValidator(),2)]),
                (nameof(DriverCommon.Situation), [(new PointerValidator(), 3)]),
                (nameof(DriverCommon.Status), [(new PointerValidator(true), 3) ])
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}