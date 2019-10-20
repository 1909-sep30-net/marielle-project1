using System;
using System.Runtime.Serialization;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Exception for invalid Address Format i.e. invalid street, city, or zipcode
    /// </summary>
    [Serializable]
    internal class InvalidAddressException : Exception
    {
        public InvalidAddressException()
        {
        }

        public InvalidAddressException(string message) : base(message)
        {
        }

        public InvalidAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}