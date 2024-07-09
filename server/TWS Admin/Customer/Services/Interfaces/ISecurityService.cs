using Customer.Managers.Records;
using Customer.Services.Records;

namespace Customer.Services.Interfaces;

public interface ISecurityService {
    public Task<Session> Authenticate(Credentials Credentials);
}
