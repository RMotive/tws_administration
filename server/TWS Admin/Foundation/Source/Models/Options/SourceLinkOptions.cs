namespace CSM_Foundation.Source.Models.Options;
public class SourceLinkOptions {
    public required string Host { get; init; }
    public required string Name { get; init; }
    public required string User { get; init; }
    public required string Password { get; init; }
    public bool Encrypt { get; init; } = false;

    public string GenerateConnectionString() {
        return $"Server={Host};" +
            $"Database={Name};" +
            $"User={User};" +
            $"Password={Password};" +
            $"Encrypt={Encrypt};";
    }
}
