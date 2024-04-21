namespace Foundation.Migration.Records;
public class MigrationConnectionOptions {
    required public string Host { get; init; }
    required public string Source { get; init; }
    required public string User { get; init; }
    required public string Password { get; init; }
    public bool Encrypt { get; init; } = false;

    public string GenerateConnectionString() {
        return $"Server={Host};" +
            $"Database={Source};" +
            $"User={User};" +
            $"Password={Password};" +
            $"Encrypt={Encrypt};";
    }
}
