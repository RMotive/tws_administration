using System;
using Customer.Contracts.Interfaces;

namespace Customer.Exceptions
{
    public class InputValidationException : SystemException, ExceptionInterface
    {
        string ExceptionSubject;
        string CustomMessage;
        Guid Unique;
        DateTime TimeMark;
        public InputValidationException(string ExceptionSubject, string CustomMessage = "") : base($"{ExceptionSubject} {CustomMessage}")
        {
            TimeMark = DateTime.UtcNow;
            Unique = Guid.NewGuid();
            this.ExceptionSubject = ExceptionSubject;
            this.CustomMessage = CustomMessage;
        }

        public Dictionary<string, dynamic> WriteEstelaResponse()
        => new Dictionary<string, dynamic>()
        {
        };
    }
}

