using Customer.Contracts.Interfaces;
using Customer.Transactions.Inputs;

namespace Customer.Transactions.Requests
{
    public class PerformLoginRequest : RequestInterface<PerformLoginInput>
    {
        public string Identity;
        public string Security;
        public PerformLoginRequest() 
        {
            this.Identity = string.Empty;
            this.Security = string.Empty;
        }

        public PerformLoginInput ToInput()
        => new PerformLoginInput(this.Identity, this.Security);
    }
}

