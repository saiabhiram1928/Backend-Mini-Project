using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    [Serializable]
    public class NoItemsInDbException : Exception
    {
        string msg = string.Empty;
        public NoItemsInDbException()
        {
            msg = "No Items Present in the db";
        }

        public NoItemsInDbException(string message) : base(message)
        {
            msg = message;
        }

        public NoItemsInDbException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoItemsInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
