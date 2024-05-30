using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    [Serializable]
    public class NoItemsInCartException :Exception
    {
           string msg = string.Empty;
            public NoItemsInCartException()
            {
                msg = "No Items in Cart,Please Add items";
            }

            public NoItemsInCartException(string message) : base(message)
            {
                msg = message;
            }

            public NoItemsInCartException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected NoItemsInCartException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
            public override string Message => msg;
        }
}
