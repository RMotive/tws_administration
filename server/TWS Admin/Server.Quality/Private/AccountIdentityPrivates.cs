using Customer;

namespace Server.Quality.Private;
public static class AccountIdentityPrivates {
    public static readonly AccountIdentityScheme CorrectScheme = new() {
        Identity = "twsm_dev",
        Password = [.. "twsmdev2023$"u8],
    };
}
