using Customer.Models;

namespace Customer;

public interface ISecurityService {
    public ForeignSessionModel InitSession();
}
