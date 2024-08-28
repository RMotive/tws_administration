using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_TrailerExternal : BQ_MigrationSet<TrailerExternal> {
    protected override Q_MigrationSet_EvaluateRecord<TrailerExternal>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<TrailerExternal>[] Container) {

        Q_MigrationSet_EvaluateRecord<TrailerExternal> success = new() {
            Mock = new() {
                Id = 1,
                Status = 1,
                Common = 1
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<TrailerExternal> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Status = 0,
                Common = 0
            },
            Expectations = [
                (nameof(TrailerExternal.Id), [(new PointerValidator(), 3)]),
                (nameof(TrailerExternal.Status), [(new PointerValidator(), 3)]),
                (nameof(TrailerExternal.Common), [(new PointerValidator(), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}