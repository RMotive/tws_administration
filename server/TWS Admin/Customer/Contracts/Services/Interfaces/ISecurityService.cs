using Customer.Models;

namespace Customer.Contracts.Services.Interfaces;

public interface ISecurityService {
    public Task<ForeignSessionModel> InitSession(AccountIdentityModel Identity);
}
