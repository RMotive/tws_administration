using Customer.Managers;
using Customer.Models;

namespace Customer;

public class SecurityService
    : ISecurityService {
    public ForeignSessionModel InitSession(AccountIdentityModel Identity) {
        SessionManager sManager = SessionManager.Int;
        
        return new ForeignSessionModel("", []);
    }
}
