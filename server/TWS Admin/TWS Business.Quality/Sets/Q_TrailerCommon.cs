using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_TrailerCommon : BQ_MigrationSet<TrailerCommon> {
    protected override Q_MigrationSet_EvaluateRecord<TrailerCommon>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<TrailerCommon>[] Container) {

        Q_MigrationSet_EvaluateRecord<TrailerCommon> success = new() {
            Mock = new() {
                Id = 1,
                Class = 1,
                Carrier = 1,
                Situation = 1,

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<TrailerCommon> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Economic = "",
                Class = 0,
                Carrier = 0,
                Situation = 0,
            },
            Expectations = [
                (nameof(TrailerCommon.Id), [(new PointerValidator(), 3)]),
                (nameof(TrailerCommon.Economic), [(new LengthValidator(),2)]),
                (nameof(TrailerCommon.Class), [(new PointerValidator(), 3)]),
                (nameof(TrailerCommon.Carrier), [(new PointerValidator(), 3)]),
                (nameof(TrailerCommon.Situation), [(new PointerValidator(), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}