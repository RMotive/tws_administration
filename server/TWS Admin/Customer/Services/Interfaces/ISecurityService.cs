using TWS_Customer.Managers.Records;
using TWS_Customer.Services.Records;

namespace TWS_Customer.Services.Interfaces;

public interface ISecurityService {
    public Task<Session> Authenticate(Credentials Credentials);
}
