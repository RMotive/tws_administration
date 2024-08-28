using CSM_Foundation.Source.Quality.Bases;
using CSM_Foundation.Source.Quality.Records;
using CSM_Foundation.Source.Validators;

using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Employee : BQ_MigrationSet<Employee> {
    protected override Q_MigrationSet_EvaluateRecord<Employee>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Employee>[] Container) {

        Q_MigrationSet_EvaluateRecord<Employee> success = new() {
            Mock = new() {
                Id = 1,
                Status = 1,
                Identification = 1,
                Curp = "",
                Address = 0,
                Approach = 0,
                Rfc = "",
                Nss = "",

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Employee> failAllCases = new() {
            Mock = new() {
                Status = 0,
                Identification = 0,
                Curp = "",
                Address = 0,
                Approach = 0,
                Rfc = "",
                Nss = "",
            },
            Expectations = [
                (nameof(Employee.Id), [(new PointerValidator(), 3)]),
                (nameof(Employee.Curp), [(new LengthValidator(), 2)]),
                (nameof(Employee.Nss), [(new LengthValidator(), 2)]),
                (nameof(Employee.Rfc), [(new LengthValidator(), 2)]),
                (nameof(Employee.Identification), [(new PointerValidator(true), 3)]),
                (nameof(Employee.Address), [(new PointerValidator(true), 3)]),
                (nameof(Employee.Approach), [(new PointerValidator(true), 3)]),
                (nameof(Employee.Status), [(new PointerValidator(true), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}