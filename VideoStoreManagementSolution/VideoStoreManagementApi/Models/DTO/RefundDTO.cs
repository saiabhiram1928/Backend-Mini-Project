using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models.DTO
{
    public class RefundDTO
    {

        public int Id { get; set; }
        public int TranasactionId { get; set; }
        public float Amount { get; set; }
        public RefundStatus Status { get; set; }
        public int OrderId { get; set; }
    }
}
