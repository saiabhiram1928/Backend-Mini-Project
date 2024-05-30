namespace VideoStoreManagementApi.Models.DTO
{
    public class ErrorDTO
    {
        public int ErrorCode { get; set; }  
        public string Message { get; set; } =string.Empty;
        public ErrorDTO(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
