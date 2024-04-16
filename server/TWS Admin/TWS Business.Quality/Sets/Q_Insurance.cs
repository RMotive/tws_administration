using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Insurance : BQ_MigrationSet<Insurance> {
    protected override Q_MigrationSet_EvaluateRecord<Insurance>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Insurance>[] Container) {

        Q_MigrationSet_EvaluateRecord<Insurance> success = new() {
            Mock = new() {
                Id = 1,
                Policy = "",
                Expiration = DateOnly.FromDateTime(new DateTime()),
                Country = ""
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Insurance> failByPointer = new() {
            Mock = new() {
                Id = 0,

            },
            Expectations = [
                (nameof(Insurance.Id), [(new PointerValidator(), 3)]),
                (nameof(Insurance.Policy), [(new RequiredValidator(), 1), (new LengthValidator(), 1)]),
                (nameof(Insurance.Country), [(new RequiredValidator(),1),(new LengthValidator(), 1)]),
            ],
        };


        Container = [.. Container, success, failByPointer];


        return Container;
    }
}