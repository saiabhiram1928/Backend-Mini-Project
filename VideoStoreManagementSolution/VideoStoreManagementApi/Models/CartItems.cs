using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models
{
    public class CartItems
    {
        public int Id { get; set; } 
         public int CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }
        public int Qty { get; set; }    
        public float Price { get; set; }
    }
}
