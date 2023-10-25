using Customer.Contracts.Interfaces;
using Customer.Exceptions;

namespace Customer.Transactions.Inputs
{
    public class PerformLoginInput : InputInterface
    {
        public byte[] Identity;
        public byte[] Security;
        public PerformLoginInput(string identity, string security)
        {
            this.Identity = Convert.FromBase64String(identity);
            this.Security = Convert.FromBase64String(security);
        }
        public PerformLoginInput(byte[] identity, byte[] security)
        {
            this.Identity = identity;
            this.Security = security;
        }

        public void Validate()
        {
            if (Identity.Length != 0 && Security.Length != 0) return;
            throw new InputValidationException($"Wrong validation for ({this.GetType()})");
        }
    }
}

