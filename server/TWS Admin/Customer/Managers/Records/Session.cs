using TWS_Security.Sets;

namespace TWS_Customer.Managers.Records;
public record Session {
    public required Guid Token { get; init; }
    public required DateTime Expiration { get; init; }
    public required string Identity { get; init; }
    public required bool Wildcard { get; init; }
    public required Permit[] Permits { get; init; }
    public required Contact Contact { get; init; }
}
