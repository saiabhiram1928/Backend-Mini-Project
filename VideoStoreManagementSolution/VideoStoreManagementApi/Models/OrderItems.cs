using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models
{
    public class OrderItems
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]   
        public Order Order { get; set; }
        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]   
        public Video Video { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }

    }
}
