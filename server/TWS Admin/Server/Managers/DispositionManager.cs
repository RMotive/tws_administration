using CSM_Foundation.Advisor.Managers;
using CSM_Foundation.Source.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Server.Managers;

public class DispositionManager : IMigrationDisposer {
    private readonly IServiceProvider Servicer;
    private readonly Dictionary<DbContext, List<ISourceSet>> DispositionStack = [];
    private bool Active = false;

    public DispositionManager(IServiceProvider Servicer) {
        this.Servicer = Servicer;
    }

    public void Push(DbContext Source, ISourceSet Record) {
        if (!Active) {
            return;
        }

        Type sourceType = Source.GetType();

        foreach (DbContext source in DispositionStack.Keys) {
            if (source.GetType() != sourceType) {
                continue;
            }

            DispositionStack[source].Add(Record);
            return;
        }
        List<ISourceSet> recordsListed = [Record];
        DispositionStack.Add(Source, recordsListed);
    }
    public void Push(DbContext Source, ISourceSet[] Records) {
        if (!Active) {
            return;
        }

        Type sourceType = Source.GetType();

        foreach (DbContext source in DispositionStack.Keys) {
            if (source.GetType() != sourceType) {
                continue;
            }

            DispositionStack[source].AddRange(Records);
            return;
        }
        List<ISourceSet> recordsListed = [.. Records.ToList()];
        DispositionStack.Add(Source, recordsListed);
    }

    public void Status(bool Active) {
        this.Active = Active;
    }

    public void Dispose() {
        if (DispositionStack.IsNullOrEmpty()) {
            AdvisorManager.Announce($"No records to dispose");
        }
        foreach (KeyValuePair<DbContext, List<ISourceSet>> disposeLine in DispositionStack) {
            using IServiceScope servicerScope = Servicer.CreateScope();
            DbContext source = disposeLine.Key;
            try {
                _ = source.Database.CanConnect();
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
            foreach (ISourceSet record in disposeLine.Value) {
                try {
                    _ = source.Remove(record);
                    _ = source.SaveChanges();
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
