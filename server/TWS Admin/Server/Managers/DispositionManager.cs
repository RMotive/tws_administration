using Foundation.Advising.Managers;
using Foundation.Migrations.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Server.Managers;

public sealed partial class DispositionManager {

    readonly Dictionary<DbContext, List<IMigrationSet>> DispositionStack = [];
    bool Active = false;
    public DispositionManager() {

    }

    public void Push(DbContext Source, IMigrationSet Record) {
        if (!Active) return;
        Type sourceType = Source.GetType();

        foreach (DbContext source in DispositionStack.Keys) {
            if (source.GetType() != sourceType)
                continue;

            DispositionStack[source].Add(Record);
            return;
        }
    }
    public void Push(DbContext Source, IMigrationSet[] Records) {
        if (!Active) return;
        Type sourceType = Source.GetType();

        foreach (DbContext source in DispositionStack.Keys) {
            if (source.GetType() != sourceType)
                continue;

            DispositionStack[source].AddRange(Records);
            return;
        }

        List<IMigrationSet> recordsListed = [.. Records.ToList()];
        DispositionStack.Add(Source, recordsListed);
    }

    public void Status(bool Active) {
        this.Active = Active;
    }

    public void Dispose() {
        foreach (KeyValuePair<DbContext, List<IMigrationSet>> disposeLine in DispositionStack) {

            DbContext source = disposeLine.Key;
            AdvisorManager.Announce($"Disposing source ({source.GetType()})");
            int corrects = 0;
            int incorrects = 0;
            foreach (IMigrationSet record in disposeLine.Value) {
                try {
                    source.Remove(record);
                    source.SaveChanges();
                    corrects++;
                    AdvisorManager.Success($"Disposed: ({record.GetType()}) | ({record.Id})");
                } catch (Exception Exep) {
                    incorrects++;
                    AdvisorManager.Warning($"No disposed: ({record.GetType()}) | ({record.Id}) |> ({Exep.Message})");
                }
            }

            if (incorrects > 0) {
                AdvisorManager.Warning($"Disposed with errors: (Errors: ({incorrects}) Successes: {corrects})");
            } else {
                AdvisorManager.Success($"Disposed: ({corrects} elements) at ({source.GetType()})");
            }
        }
    }
}
