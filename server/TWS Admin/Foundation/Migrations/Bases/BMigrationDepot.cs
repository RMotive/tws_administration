using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Records;

using Microsoft.EntityFrameworkCore;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationDepot<TMigrationSource, TMigrationSet>
    : IMigrationDepot<TMigrationSet>
    where TMigrationSource : BMigrationSource<TMigrationSource>
    where TMigrationSet : class, IMigrationSet {

    readonly TMigrationSource Source;
    readonly DbSet<TMigrationSet> Set;
    public BMigrationDepot(TMigrationSource source) {
        this.Source = source;
        Set = Source.Set<TMigrationSet>();
    }

    #region Create Interface
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Set"></param>
    /// <returns></returns>
    public async Task<TMigrationSet> Create(TMigrationSet Set) {
        Set.EvaluateWrite();
        await this.Set.AddAsync(Set);
        await Source.SaveChangesAsync();
        Source.ChangeTracker.Clear();
        return Set;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sets"></param>
    /// <param name="Sync"></param>
    /// <returns></returns>
    public async Task<MigrationTransactionResult<TMigrationSet>> Create(TMigrationSet[] Sets, bool Sync = false) {
        TMigrationSet[] safe = [];
        MigrationTransactionFailure[] fails = [];

        foreach (TMigrationSet set in Sets) {
            try {
                set.EvaluateWrite();
                safe = [.. safe, set];
            } catch (XBMigrationSet_Evaluate) {
                MigrationTransactionFailure fail = new();
                fails = [.. fails, fail];
            }
        }

        await this.Set.AddRangeAsync(safe);
        return new(safe, []);
    }

    #endregion
}