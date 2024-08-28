using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Section : BQ_MigrationSet<Section> {
    protected override Q_MigrationSet_EvaluateRecord<Section>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Section>[] Container) {

        Q_MigrationSet_EvaluateRecord<Section> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",
                Yard = 0,
                Status = 0

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Section> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Name = "",
                Yard = 0,
                Status = 0
            },
            Expectations = [
                (nameof(Section.Id), [(new PointerValidator(), 3)]),
                (nameof(Section.Name), [(new LengthValidator(), 2)]),
                (nameof(Section.Yard), [(new PointerValidator(true), 3)]),
                (nameof(Section.Status), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}