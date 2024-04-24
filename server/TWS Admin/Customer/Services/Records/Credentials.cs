namespace Customer.Services.Records;
public record Credentials {
    required public string Identity { get; init; }
    required public byte[] Password { get; init; }
}
