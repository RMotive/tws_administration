using System.Text.Json.Serialization;

using Foundation.Contracts.Modelling.Bases;

namespace Customer;

public class AccountIdentityScheme
    : BScheme<AccountIdentityScheme, AccountIdentityModel> {
    [JsonRequired]
    public string Identity { get; set; } = string.Empty;
    [JsonRequired]
    public byte[] Password { get; set; } = [];

    protected override AccountIdentityModel Generate()
    => new(Identity, Password);
}

