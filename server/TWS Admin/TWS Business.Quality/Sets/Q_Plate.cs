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
                Expiration = DateOnly.FromDateTime(new DateTime())

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Plate> failByPointer = new() {
            Mock = new() {
                Id = 0,

            },
            Expectations = [
                (nameof(Plate.Id), [(new PointerValidator(), 3)]),
                (nameof(Plate.Identifier), [(new RequiredValidator(), 1), (new LengthValidator(), 1)]),
                (nameof(Plate.State), [(new RequiredValidator(), 1), (new LengthValidator(), 1)]),
                (nameof(Plate.Country), [(new RequiredValidator(), 1), (new LengthValidator(), 1)]),

            ],
        };


        Container = [.. Container, success, failByPointer];


        return Container;
    }
}