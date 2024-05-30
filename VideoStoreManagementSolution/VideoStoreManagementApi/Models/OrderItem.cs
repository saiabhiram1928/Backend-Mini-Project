using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order Order { get; set; }
        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]   
        public Video Video { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }

    }
}
