using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Address : BQ_MigrationSet<Address> {
    protected override Q_MigrationSet_EvaluateRecord<Address>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Address>[] Container) {

        Q_MigrationSet_EvaluateRecord<Address> success = new() {
            Mock = new() {
                Id = 1,
                Country = "",

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Address> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Country = "",

            },
            Expectations = [
                (nameof(Insurance.Id), [(new PointerValidator(), 3)]),
                (nameof(Insurance.Country), [(new LengthValidator(), 2)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}