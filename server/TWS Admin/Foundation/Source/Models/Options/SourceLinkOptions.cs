namespace CSMFoundation.Source.Models.Options;
public class SourceLinkOptions {
    required public string Host { get; init; }
    required public string Name { get; init; }
    required public string User { get; init; }
    required public string Password { get; init; }
    public bool Encrypt { get; init; } = false;

    public string GenerateConnectionString() {
        return $"Server={Host};" +
            $"Database={Name};" +
            $"User={User};" +
            $"Password={Password};" +
            $"Encrypt={Encrypt};";
    }
}
