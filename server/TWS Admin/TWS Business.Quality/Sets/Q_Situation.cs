using Foundation.Migrations.Quality.Bases;
using Foundation.Migrations.Quality.Records;
using Foundation.Migrations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWS_Business.Sets;

namespace TWS_Business.Quality.Sets;
public class Q_Situation : BQ_MigrationSet<Situation> {
    protected override Q_MigrationSet_EvaluateRecord<Situation>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<Situation>[] Container) {

        Q_MigrationSet_EvaluateRecord<Situation> success = new() {
            Mock = new() {
                Id = 1,
                Name = "",

            },
            Expectations = [],
        };
        Q_MigrationSet_EvaluateRecord<Situation> failAllCases = new() {
            Mock = new() {
                Id = 0,
                Name = "E232342343jHDJS343jfser$435435234213",
            },
            Expectations = [
                (nameof(Situation.Id), [(new PointerValidator(), 3)]),
                (nameof(Situation.Name), [(new LengthValidator(), 3)]),
            ],
        };


        Container = [.. Container, success, failAllCases];


        return Container;
    }
}
