using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;
using Foundation.Records.Exceptions;
using Newtonsoft.Json;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Truck : BQ_MigrationSet<Truck> {
    protected override Q_MigrationSet_EvaluateRecord<Truck>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Truck>[] Container) {
        const string Vin = "";
        const string Motor = "";
        const string Sct = "";

        Q_MigrationSet_EvaluateRecord<Truck> success = new() {
            Mock = new() {
                Id = 1,
                Vin = Vin,
                Plate = 2,
                Manufacturer = 3,
                Motor = Motor,
                Sct = Sct,
                Maintenance = 4,
                Situation = 0,
                Insurance = 5,
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Truck> failAllCases = new() {
            Mock = new() {
                Id = -1,
                Vin = Vin,
                Plate = 0,
                Manufacturer = 0,
                Motor = Motor,
                Sct = Sct,
                Maintenance = 0,
                Situation = 0,
                Insurance = 0,
            },
            Expectations = [
                (nameof(Truck.Id), [(new PointerValidator(), 3) ]),
                (nameof(Truck.Plate),[(new PointerValidator(),1)]),
                (nameof(Truck.Manufacturer), [(new PointerValidator(),1)]),
                (nameof(Truck.Vin), [((new LengthValidator(),2))]),
                (nameof(Truck.Motor), [(new LengthValidator(),2)]),
                (nameof(Truck.Sct), [(new LengthValidator(),2)]),
                (nameof(Truck.Maintenance), [(new PointerValidator(),1)]),
                (nameof(Truck.Situation), [(new PointerValidator(),1)]),
                (nameof(Truck.Insurance),[(new PointerValidator(),1)])
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}