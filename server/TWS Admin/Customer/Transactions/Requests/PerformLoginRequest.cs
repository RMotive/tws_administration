using Customer.Contracts.Interfaces;
using Customer.Transactions.Inputs;

namespace Customer.Transactions.Requests
{
    public class PerformLoginRequest : RequestInterface<PerformLoginInput>
    {
        public string Identity { get; set; }
        public string Security { get; set; }
        public PerformLoginRequest() 
        {
            this.Identity = string.Empty;
            this.Security = string.Empty;
        }

        public PerformLoginInput ToInput()
        => new(this.Identity, this.Security);
    }
}

