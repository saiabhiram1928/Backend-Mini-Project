using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions 
{
    public class QunatityOutOfStockException : Exception
    {
        string msg = string.Empty;
        public QunatityOutOfStockException()
        {
            msg = "Given Quantity cant be delivered";
        }

        public QunatityOutOfStockException(string message) : base(message)
        {
            msg = message;
        }

        public QunatityOutOfStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QunatityOutOfStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
