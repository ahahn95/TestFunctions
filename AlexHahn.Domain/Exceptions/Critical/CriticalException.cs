using System;

namespace Domain.Exceptions.Critical
{
    public class CriticalException : Exception
    {
        public CriticalException()
        {
        }

        public CriticalException(string message) : base(message)
        {
        }

        public CriticalException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}