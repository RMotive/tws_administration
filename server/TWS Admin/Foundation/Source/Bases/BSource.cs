using System.Runtime.CompilerServices;

using CSM_Foundation.Advisor.Managers;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Utils;

using Microsoft.EntityFrameworkCore;

namespace CSM_Foundation.Source.Bases;
public abstract class BSource<TSource>
    : DbContext, IMigrationSource
    where TSource : DbContext {

    private readonly SourceLinkOptions Connection;

    public BSource([CallerFilePath] string? callerPath = null)
        : base() {
        Connection = MigrationUtils.Retrieve(callerPath);
        ValidateHealth();
    }
    public BSource(DbContextOptions<TSource> Options, [CallerFilePath] string? callerPath = null)
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
            _ = set.EvaluateDefinition();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        _ = optionsBuilder.UseSqlServer(Connection.GenerateConnectionString());
    }

    /// <summary>
    /// 
    /// </summary>
    private void ValidateHealth() {
        AdvisorManager.Announce($"[{GetType().Name}] Running connection checker...");

        if (Database.CanConnect()) {
            AdvisorManager.Success($"[{GetType().Name}] connection successfuly stablished");
        }
    }
}
