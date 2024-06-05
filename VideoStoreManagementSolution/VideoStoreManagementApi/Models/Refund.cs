using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Refund
    {
        public int Id { get; set; } 
        public int TranasactionId { get; set; }
        public float Amount {  get; set; }  
        public RefundStatus  Status { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
