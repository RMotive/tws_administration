using System.Text;
using Customer.Contracts.Interfaces;
using Customer.Exceptions;

namespace Customer.Transactions.Inputs
{
    public class PerformLoginInput : InputInterface
    {
        public byte[] Identity { get; }
        public byte[] Security { get; }
        public PerformLoginInput(string identity, string security)
        {
            this.Identity = Encoding.ASCII.GetBytes(identity);
            this.Security = Encoding.ASCII.GetBytes(security);
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

