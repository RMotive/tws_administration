using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Maintenance : BQ_MigrationSet<Maintenance> {
    protected override Q_MigrationSet_EvaluateRecord<Maintenance>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Maintenance>[] Container) {

        Q_MigrationSet_EvaluateRecord<Maintenance> success = new() {
            Mock = new() {
                Id = 1,
                Anual = DateOnly.FromDateTime(new DateTime()),
                Trimestral = DateOnly.FromDateTime(new DateTime()),
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Maintenance> failAllCases = new() {
            Mock = new() {
                Id = 0,

            },
            Expectations = [
                (nameof(Maintenance.Id), [(new PointerValidator(), 3)]),

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}