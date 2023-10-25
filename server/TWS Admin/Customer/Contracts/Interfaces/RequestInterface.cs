using System;
namespace Customer.Contracts.Interfaces
{
    public interface RequestInterface<TInput>
    {
        public TInput ToInput();
    }
}

