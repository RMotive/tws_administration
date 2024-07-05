using System.Runtime.CompilerServices;

using Foundation.Advising.Managers;
using Foundation.Migration.Records;
using Foundation.Migration.Utils;
using Foundation.Migrations.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationSource<TSource>
    : DbContext, IMigrationSource
    where TSource : DbContext {

    private readonly MigrationConnectionOptions Connection;

    public BMigrationSource([CallerFilePath] string? callerPath = null) {
        Connection = MigrationUtils.Retrieve(callerPath);
        ValidateHealth();
    }
    public BMigrationSource(DbContextOptions<TSource> Options, [CallerFilePath] string? callerPath = null)
        : base(Options) {
        Connection = MigrationUtils.Retrieve(callerPath);
        ValidateHealth();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract IMigrationSet[] EvaluateFactory();
    /// <summary>
    /// 
    /// </summary>
    public void Evaluate() {
        IMigrationSet[] sets = EvaluateFactory();

        foreach (IMigrationSet set in sets) {
            set.EvaluateDefinition();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(Connection.GenerateConnectionString());
    /// <summary>
    /// 
    /// </summary>
    void ValidateHealth() {
        AdvisorManager.Announce($"[{GetType().Name}] Running connection checker...");

        if (Database.CanConnect()) {
            AdvisorManager.Success($"[{GetType().Name}] connection successfuly stablished");
        }
    }
}
