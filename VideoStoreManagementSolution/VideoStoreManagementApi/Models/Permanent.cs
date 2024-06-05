using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models
{
    public class Permanent
    {
        public int Id { get; set; } 
        public int OrderId {  get; set; }
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order Order { get; set; }    
        public int TotalQty { get; set; }
        public float TotalPrice { get; set; }

    }
}
