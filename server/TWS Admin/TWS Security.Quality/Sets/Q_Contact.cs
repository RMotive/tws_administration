using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Security.Sets;

namespace TWS_Security.Quality.Sets;
public class Q_Contact
    : BQ_MigrationSet<Contact> {
    protected override Q_MigrationSet_EvaluateRecord<Contact>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Contact>[] Container) {
        Q_MigrationSet_EvaluateRecord<Contact> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",
                Lastname = "",
                Email = "",
                Phone = ""
            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Contact> failure = new() {
            Mock = new() {
                Id = 0,
                Name = "",
                Lastname = "",
                Email = "",
                Phone = ""
            },
            Expectations = [
                (nameof(Contact.Id), [(new PointerValidator(), 3)]),
                (nameof(Contact.Name), [(new LengthValidator(), 2)]),
                (nameof(Contact.Lastname), [(new LengthValidator(), 2)]),
                (nameof(Contact.Email), [(new LengthValidator(), 2)]),
                (nameof(Contact.Phone), [(new LengthValidator(), 2)])
            ],
        };

        return [.. Container, success, failure];
    }
}
