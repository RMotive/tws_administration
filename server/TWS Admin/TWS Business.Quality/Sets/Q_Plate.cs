using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Plate : BQ_MigrationSet<Plate> {
    protected override Q_MigrationSet_EvaluateRecord<Plate>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Plate>[] Container) {

        Q_MigrationSet_EvaluateRecord<Plate> success = new() {
            Mock = new() {
                Id = 1,
                Identifier = "",
                State = "",
                Country = "",
                Expiration = DateOnly.FromDateTime(new DateTime()),
                Truck = 1


            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Plate> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Identifier = "",
                State = "",
                Country = "",
                Truck = 0,
                Status = 0
            },
            Expectations = [
                (nameof(Plate.Id), [(new PointerValidator(), 3)]),
                (nameof(Plate.Identifier), [(new LengthValidator(), 2)]),
                (nameof(Plate.State), [(new LengthValidator(), 2)]),
                (nameof(Plate.Country), [(new LengthValidator(), 2)]),
                (nameof(Plate.Truck), [(new PointerValidator(true), 3)]),
                (nameof(Plate.Status), [(new PointerValidator(true), 3)]),

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}