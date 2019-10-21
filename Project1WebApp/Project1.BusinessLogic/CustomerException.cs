using System;
using System.Runtime.Serialization;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Exceptions in customer class mostly from invalid or null inputs to properties 
    /// </summary>
    [Serializable]
    public class CustomerException : Exception
    {
        public CustomerException()
        {
        }

        public CustomerException(string message) : base(message)
        {
        }

        public CustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}