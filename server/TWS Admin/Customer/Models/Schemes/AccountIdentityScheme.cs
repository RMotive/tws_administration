using Foundation;
using Foundation.Contracts.Modelling.Bases;
using Microsoft.IdentityModel.Tokens;

namespace Customer;

public class AccountIdentityScheme
    : BScheme<AccountIdentityScheme, AccountIdentityModel> {
    public string Identity {get; set; } = string.Empty;
    public byte[] Password {get; set;} = [];
    
    protected override AccountIdentityModel Generate() {
        return new AccountIdentityModel(Identity, Password);
    }
    protected override List<ISchemeIntegrityRule> GenerateRules() {

        return [
            new SchemeIntegrityRule<string>(HookProperty(nameof(Identity)), 
                (string I) => {
                    if(I.IsNullOrEmpty()) return NULL_EMPTY_REASON;
                    return null;
            }),
            new SchemeIntegrityRule<byte[]>(HookProperty(nameof(Password)), 
                (byte[] I) => {
                    if(I.IsNullOrEmpty()) return NULL_EMPTY_REASON;
                    return null;
            }),
        ];
    }
}
