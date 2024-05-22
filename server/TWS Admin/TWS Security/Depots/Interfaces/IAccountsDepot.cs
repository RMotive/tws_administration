using TWS_Security.Sets;

namespace TWS_Security.Depots.Interfaces;
public interface IAccountsDepot {
    public Task<Permit[]> GetPermits(int Account);
}
