using System;
namespace Customer.Contracts.Interfaces
{
    public interface ExceptionInterface
    {
        public void WriteLog() => throw new NotImplementedException();
        public Dictionary<string, dynamic> WriteEstelaResponse();
    }
}

