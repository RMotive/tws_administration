using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Manufacturer : BQ_MigrationSet<Manufacturer> {
    protected override Q_MigrationSet_EvaluateRecord<Manufacturer>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Manufacturer>[] Container) {

        Q_MigrationSet_EvaluateRecord<Manufacturer> success = new() {
            Mock = new() {
                Id = 1,
                Model = "",
                Brand = "",
                Year = DateOnly.FromDateTime(new DateTime())
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Manufacturer> failByPointer = new() {
            Mock = new() {
                Id = 0,
            },
            Expectations = [
                (nameof(Manufacturer.Id), [(new PointerValidator(), 3)]),
                (nameof(Manufacturer.Model), [(new RequiredValidator(), 1), (new LengthValidator(), 1)]),
                (nameof(Manufacturer.Brand), [(new RequiredValidator(), 1), (new LengthValidator(), 1)]),
            ],
        };


        Container = [.. Container, success, failByPointer];


        return Container;
    }
}