using CSMFoundation.Migration.Quality.Bases;
using CSMFoundation.Migration.Quality.Records;
using CSMFoundation.Migration.Validators;

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
        Q_MigrationSet_EvaluateRecord<Manufacturer> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Model = "",
                Brand = "",
            },
            Expectations = [
                (nameof(Manufacturer.Id), [(new PointerValidator(), 3)]),
                (nameof(Manufacturer.Model), [(new LengthValidator(), 2)]),
                (nameof(Manufacturer.Brand), [(new LengthValidator(), 2)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}