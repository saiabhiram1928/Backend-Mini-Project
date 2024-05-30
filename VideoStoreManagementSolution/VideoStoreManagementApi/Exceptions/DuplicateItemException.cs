using System.Runtime.Serialization;

namespace VideoStoreManagementApi.Exceptions
{
    [Serializable]
    public class DuplicateItemException : Exception
    {
        string msg = string.Empty;
        public DuplicateItemException()
        {
            msg = "The item with given Id Already Exists";
        }

        public DuplicateItemException(string message) : base(message)
        {
            msg = message;
        }

        public DuplicateItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
