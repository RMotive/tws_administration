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
                
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Maintenance> failByPointer = new() {
            Mock = new() {
                Id = -1,
               
            },
            Expectations = [
                (nameof(Maintenance.Id), [(new PointerValidator(), 1)]),
                (nameof(Maintenance.Anual), [(new RequiredValidator(), 1)]),
                (nameof(Maintenance.Trimestral), [(new RequiredValidator(), 1)]),

            ],
        };


        Container = [.. Container, success, failByPointer];


        return Container;
    }
}