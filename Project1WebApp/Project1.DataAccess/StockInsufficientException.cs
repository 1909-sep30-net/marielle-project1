using System;
using System.Runtime.Serialization;

namespace Project1.DataAccess
{
    [Serializable]
    internal class StockInsufficientException : Exception
    {
        public StockInsufficientException()
        {
        }

        public StockInsufficientException(string message) : base(message)
        {
        }

        public StockInsufficientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StockInsufficientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}