using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Truck : BQ_MigrationSet<Truck> {
    protected override Q_MigrationSet_EvaluateRecord<Truck>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Truck>[] Container) {
        const string Vin = "";
        const string Motor = "";
        PointerValidator pointer = new(true,false);

        Q_MigrationSet_EvaluateRecord<Truck> success = new() {
            Mock = new() {
                Id = 1,
                Vin = Vin,
                Manufacturer = 3,
                Motor = Motor,
                Sct = 1,
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
                Manufacturer = 0,
                Hp = 0,
                Modified = DateTime.Now,
                Status = 0,
                Motor = Motor,
                Sct = 0,
                Maintenance = 0,
                Situation = 0,
                Insurance = 0
            },
            Expectations = [
                (nameof(Truck.Id), [(new PointerValidator(), 3) ]),
                (nameof(Truck.Vin), [(new LengthValidator(),2)]),
                (nameof(Truck.Motor), [(new LengthValidator(),2)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}