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
                Economic = "",
                Maintenance = 4,
                Situation = 0,
                Insurance = 5,
                Status = 1
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Truck> failAllCases = new() {
            Mock = new() {
                Id = -1,
                Vin = Vin,
                Economic = "",
                Manufacturer = 0,
                Motor = Motor,
                Maintenance = 0,
                Situation = 0,
                Insurance = 0,
                Status = 0
            },
            Expectations = [
                (nameof(Truck.Id), [(new PointerValidator(), 3) ]),
                (nameof(Truck.Vin), [(new LengthValidator(),2)]),
                (nameof(Truck.Economic), [(new LengthValidator(), 2)]),
                (nameof(Truck.Status), [(new PointerValidator(), 3) ]),
                (nameof(Truck.Carrier), [(new PointerValidator(), 3) ])

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}