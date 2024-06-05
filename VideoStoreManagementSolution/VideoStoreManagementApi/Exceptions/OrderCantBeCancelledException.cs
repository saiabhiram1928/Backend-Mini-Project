using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    public class OrderCantBeCancelledException : Exception
    {

        string msg = string.Empty;
        public OrderCantBeCancelledException()
        {
            msg = "Order Cant be Cancelled";
        }

        public OrderCantBeCancelledException(string message) : base(message)
        {
            msg = message;
        }

        public OrderCantBeCancelledException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderCantBeCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
