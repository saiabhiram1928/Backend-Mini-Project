using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    [Serializable]
    public class DbException : Exception
    {
        string msg = string.Empty;
        public DbException()
        {
            msg = "Something Went Down,Please try After Some Time";
        }

        public DbException(string message) : base(message)
        {
            msg = message;
        }

        public DbException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
