using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;

using Microsoft.EntityFrameworkCore;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationDepot<TMigrationSource, TMigrationSet>
    : IMigrationDepot<TMigrationSet>
    where TMigrationSource : BMigrationSource<TMigrationSource>
    where TMigrationSet : class, IMigrationSet {

    TMigrationSource Source;
    DbSet<TMigrationSet> Set;

    public BMigrationDepot(TMigrationSource source) {
        this.Source = source;
        Set = Source.Set<TMigrationSet>();
    }

    public async Task<TMigrationSet> Create(TMigrationSet Set) {
        Set.Evaluate();
        await this.Set.AddAsync(Set);
        return Set;
    }

    public async Task<MigrationTransactionResult_Critical<TMigrationSet>> Create(TMigrationSet[] Sets) {
        TMigrationSet[] safe = [];


        foreach(TMigrationSet set in Sets) {
            try {
                set.Evaluate();
                safe = [..safe, set];
            } catch (XBMigrationSet_Evaluate x) {

            }
        }

        await this.Set.AddRangeAsync(safe);
        return new(safe, []);
    }
}
