using System.Runtime.CompilerServices;

using Foundation.Managers;
using Foundation.Models;

using Microsoft.EntityFrameworkCore;

namespace Foundation.Contracts.Datasources.Bases;
public abstract class BEntityFramework<TSource>
    : DbContext
    where TSource : DbContext {

    private readonly DatasourceConnectionModel Connection;

    public BEntityFramework([CallerFilePath] string? callerPath = null) {
        Connection = DatasourceConnectionManager.Load(callerPath);
    }
    public BEntityFramework(DbContextOptions<TSource> Options, [CallerFilePath] string? callerPath = null)
        : base(Options) {
        Connection = DatasourceConnectionManager.Load(callerPath);
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
