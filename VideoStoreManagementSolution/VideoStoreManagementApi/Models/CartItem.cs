using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models
{
    public class CartItem
    {
        public int Id { get; set; } 
         public int CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        [JsonIgnore]
        public Cart Cart { get; set; }
        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }
        public int Qty { get; set; }    
        public float Price { get; set; }
    }
}
