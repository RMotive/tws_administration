using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Contact : BQ_MigrationSet<Contact> {
    protected override Q_MigrationSet_EvaluateRecord<Contact>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Contact>[] Container) {

        Q_MigrationSet_EvaluateRecord<Contact> success = new() {
            Mock = new() {
                Id = 1,
                Email = "",
                Status = 1

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Contact> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Email = "",
                Status = 0
            },
            Expectations = [
                (nameof(Contact.Id), [(new PointerValidator(), 3)]),
                (nameof(Contact.Email), [(new LengthValidator(), 2)]),
                (nameof(Contact.Status), [(new PointerValidator(true), 3)]),

            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}