using TWS_Security.Sets;

namespace Customer.Managers.Records;
public record Session {
    required public Guid Token { get; init; }
    required public DateTime Expiration { get; init; }
    required public string Identity { get; init; }
    required public bool Wildcard { get; init; }
    required public Permit[] Permits { get; init; }
    required public Contact Contact { get; init; }
}
