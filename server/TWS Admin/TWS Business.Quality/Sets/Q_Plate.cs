using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;
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
                Truck = 0
            },
            Expectations = [
                (nameof(Plate.Id), [(new PointerValidator(), 3)]),
                (nameof(Plate.Identifier), [(new LengthValidator(), 2)]),
                (nameof(Plate.State), [(new LengthValidator(), 2)]),
                (nameof(Plate.Country), [(new LengthValidator(), 2)]),
                (nameof(Plate.Truck), [(new PointerValidator(), 1)]),

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}