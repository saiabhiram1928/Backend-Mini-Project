using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    [Serializable]
    public class NoSuchItemInDbException : Exception
    {
        string msg = string.Empty;
        public NoSuchItemInDbException()
        {
            msg = "No Such Item with Given Id in Db";
        }

        public NoSuchItemInDbException(string message) : base(message)
        {
            msg = message;
        }

        public NoSuchItemInDbException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchItemInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
