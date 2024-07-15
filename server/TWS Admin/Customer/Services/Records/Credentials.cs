namespace TWS_Customer.Services.Records;
public record Credentials {
    public required string Identity { get; init; }
    public required byte[] Password { get; init; }
}
