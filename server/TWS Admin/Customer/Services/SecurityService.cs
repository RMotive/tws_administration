using Customer.Contracts.Interfaces;
using Customer.Transactions.Inputs;

namespace Customer.Services
{
    public class SecurityService : IService
    {
        public SecurityService()
        {
        }

        public dynamic PerformLogin(PerformLoginInput input)
        {
            input.Validate();
            return false;
        }
    }
}

