using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    [Serializable]
    public class UnauthorizedUserException:Exception
    {
        string msg = string.Empty;
        public UnauthorizedUserException()
        {
            msg = "Invalid UserName Or Password";
        }

        public UnauthorizedUserException(string message) : base(message)
        {
            msg =message;
        }

        public UnauthorizedUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
