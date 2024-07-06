using CSMFoundation.Advising.Managers;
using CSMFoundation.Migration.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Server.Managers;

public class DispositionManager : IMigrationDisposer {

    readonly IServiceProvider Servicer;
    readonly Dictionary<DbContext, List<IMigrationSet>> DispositionStack = [];
    bool Active = false;

    public DispositionManager(IServiceProvider Servicer) {
        this.Servicer = Servicer;
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
        List<IMigrationSet> recordsListed = [Record];
        DispositionStack.Add(Source, recordsListed);
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
        if (DispositionStack.IsNullOrEmpty()) {
            AdvisorManager.Announce($"No records to dispose");
        }
        foreach (KeyValuePair<DbContext, List<IMigrationSet>> disposeLine in DispositionStack) {
            using IServiceScope servicerScope = Servicer.CreateScope();
            DbContext source = disposeLine.Key;
            try {
                source.Database.CanConnect();
            } catch (ObjectDisposedException) {
                source = (DbContext)servicerScope.ServiceProvider.GetRequiredService(source.GetType());
            }

            AdvisorManager.Announce($"Disposing source ({source.GetType()})");
            if (disposeLine.Value.IsNullOrEmpty()) {
                AdvisorManager.Announce($"No records to dispose");
                continue;
            }
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
