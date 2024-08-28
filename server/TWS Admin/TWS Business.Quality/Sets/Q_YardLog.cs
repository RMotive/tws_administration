using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_YardLog : BQ_MigrationSet<YardLog> {
    protected override Q_MigrationSet_EvaluateRecord<YardLog>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<YardLog>[] Container) {

        Q_MigrationSet_EvaluateRecord<YardLog> success = new() {
            Mock = new() {
                Id = 1,
                LoadType = 0,
                Section = 0

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<YardLog> failAllCases = new() {
            Mock = new() {
                Id = 0,
                LoadType = 0,
                Section = 0,
                Guard = 1,
                Gname = "",
                FromTo = ""
            },
            Expectations = [
                (nameof(YardLog.Id), [(new PointerValidator(), 3)]),
                (nameof(YardLog.TTPicture), [(new LengthValidator(), 1)]),
                (nameof(YardLog.Gname), [(new LengthValidator(), 2)]),
                (nameof(YardLog.FromTo), [(new LengthValidator(), 2)]),
                (nameof(YardLog.LoadType), [(new PointerValidator(true), 3)]),
                (nameof(YardLog.Section), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}