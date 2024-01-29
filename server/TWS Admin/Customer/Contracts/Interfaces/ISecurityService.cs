using Customer.Models;

namespace Customer;

public interface ISecurityService {
    public Task<ForeignSessionModel> InitSession(AccountIdentityModel Identity);
}
