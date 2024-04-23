using Customer.Services.Records;

namespace Customer.Services.Interfaces;

public interface ISecurityService { 
    public Task<Privileges> Authenticate(Credentials Credentials);
}
