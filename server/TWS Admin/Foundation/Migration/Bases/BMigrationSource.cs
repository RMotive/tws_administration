using System.Runtime.CompilerServices;

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
    }
    public BMigrationSource(DbContextOptions<TSource> Options, [CallerFilePath] string? callerPath = null)
        : base(Options) {
        Connection = MigrationUtils.Retrieve(callerPath);
    }

    protected abstract IMigrationSet[] EvaluateFactory();
    public void Evaluate() {
        IMigrationSet[] sets = EvaluateFactory();

        foreach (IMigrationSet set in sets) {
            set.EvaluateDefinition();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(Connection.GenerateConnectionString());
}
