using Customer.Contracts.Interfaces;

using TWS_Security;

namespace Customer.Services;

/// <summary>
///     Serves all operations related to Accounts
/// </summary>
public class SecurityService
{
    /// <summary>
    ///     Internal dependency to TWS Security datasource
    /// </summary>
    private readonly TWSSecuritySource source;

    public SecurityService(TWSSecuritySource Source)
    {
        source = Source;
    }

    
}
