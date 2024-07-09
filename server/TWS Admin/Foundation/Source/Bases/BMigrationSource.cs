using System.Runtime.CompilerServices;

using CSMFoundation.Advising.Managers;
using CSMFoundation.Migration.Utils;
using CSMFoundation.Migration.Interfaces;

using Microsoft.EntityFrameworkCore;
using CSMFoundation.Source.Models.Options;

namespace CSMFoundation.Migration.Bases;
public abstract class BMigrationSource<TSource>
    : DbContext, IMigrationSource
    where TSource : DbContext {

    private readonly SourceLinkOptions Connection;

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
    protected abstract ISourceSet[] EvaluateFactory();
    /// <summary>
    /// 
    /// </summary>
    public void Evaluate() {
        ISourceSet[] sets = EvaluateFactory();

        foreach (ISourceSet set in sets) {
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
