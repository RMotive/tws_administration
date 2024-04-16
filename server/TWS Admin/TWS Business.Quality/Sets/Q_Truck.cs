using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;
using Foundation.Records.Exceptions;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Truck : BQ_MigrationSet<Truck> {
    protected override Q_MigrationSet_EvaluateRecord<Truck>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Truck>[] Container) {
        const string Vin = "";
        const string Motor = "";
        const string Sct = "";
        const string Situation = "";

        Q_MigrationSet_EvaluateRecord<Truck> success = new() {
            Mock = new() {
                Id = 1,
                Vin = Vin,
                Plate = 2,
                Manufacturer = 3,
                Motor = Motor,
                Sct = Sct,
                Maintenance = 4,
                Situation = Situation,
                Insurance = 5,
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Truck> failByPointer = new() {
            Mock = new() {
                Id = -1,
            },
            Expectations = [
                (nameof(Truck.Id), [(new PointerValidator(), 3) ]),
                (nameof(Truck.Vin), [(new RequiredValidator(),1), (new LengthValidator(),1)]),
                (nameof(Truck.Motor), [ (new RequiredValidator(),1), (new LengthValidator(),1)]),
                (nameof(Truck.Sct), [(new RequiredValidator(),1), (new LengthValidator(),1)]),
                (nameof(Truck.Situation), [ (new RequiredValidator(),1), (new LengthValidator(),1)]),
            ],
        };


        Container = [.. Container, success, failByPointer];


        return Container;
    }
}