using System.ComponentModel.DataAnnotations;

namespace VideoStoreManagementApi.Models.DTO
{
    public class ErrorDTO
    {
        [Required]
        public int ErrorCode { get; set; }
        [Required]
        public string Message { get; set; } =string.Empty;
        public ErrorDTO(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
