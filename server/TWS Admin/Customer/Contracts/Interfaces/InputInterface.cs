using System;
using Customer.Exceptions;

namespace Customer.Contracts.Interfaces
{
    public interface InputInterface
    {
        /// <summary>
        ///     This method implementation should throw exceptions in cases the Input
        ///     class is not being validated.
        /// </summary>
        public void Validate();
    }
}

