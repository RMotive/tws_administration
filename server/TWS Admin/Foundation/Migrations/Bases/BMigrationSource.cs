using System.Runtime.CompilerServices;

using Foundation.Managers;
using Foundation.Migrations.Interfaces;
using Foundation.Models;

using Microsoft.EntityFrameworkCore;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationSource<TSource>
    : DbContext
    where TSource : DbContext {

    private readonly DatasourceConnectionModel Connection;

    public BMigrationSource([CallerFilePath] string? callerPath = null) {
        Connection = DatasourceConnectionManager.Load(callerPath);
    }
    public BMigrationSource(DbContextOptions<TSource> Options, [CallerFilePath] string? callerPath = null)
        : base(Options) {
        Connection = DatasourceConnectionManager.Load(callerPath);
    }

    protected abstract IMigrationSet[] EvaluateFactory();
    public void Evaluate() {
        IMigrationSet[] sets = EvaluateFactory();

        foreach(IMigrationSet set in sets) {
            set.Evaluate(true);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string ConnectionString =
            $"Server={Connection.Host};" +
            $"Database={Connection.Database};" +
            $"User={Connection.User};" +
            $"Password={Connection.Password};" +
            $"Encrypt={Connection.Encrypted};";


        optionsBuilder.UseSqlServer(ConnectionString);
    }
}
