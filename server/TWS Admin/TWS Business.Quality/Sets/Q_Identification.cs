using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Identification : BQ_MigrationSet<Identification> {
    protected override Q_MigrationSet_EvaluateRecord<Identification>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Identification>[] Container) {

        Q_MigrationSet_EvaluateRecord<Identification> success = new() {
            Mock = new() {
                Id = 1,
                Status = 1,
                Name = "",
                FatherLastname = "",
                MotherLastName = ""

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Identification> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Status = 0,
                Name = "",
                FatherLastname = "",
                MotherLastName = ""
            },
            Expectations = [
                (nameof(Identification.Id), [(new PointerValidator(), 3)]),
                (nameof(Identification.Name), [(new LengthValidator(), 2)]),
                (nameof(Identification.FatherLastname), [(new LengthValidator(), 2)]),
                (nameof(Identification.MotherLastName), [(new LengthValidator(), 2)]),
                (nameof(Identification.Status), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}