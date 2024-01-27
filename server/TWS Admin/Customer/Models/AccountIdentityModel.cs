using Foundation.Contracts.Modelling.Bases;

namespace Customer;

public class AccountIdentityModel
    : BModel {
    public string Identity {get; private set;} 
    public byte[] Password {get; private set;}

    public AccountIdentityModel(string identity, byte[] password) {
        Identity = identity;
        Password = password;
    }
}
