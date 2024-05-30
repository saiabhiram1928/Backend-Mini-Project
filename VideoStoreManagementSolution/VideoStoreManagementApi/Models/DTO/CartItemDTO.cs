using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models.DTO
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        
        public int VideoId { get; set; }   
        public string VideoTittle { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
       
    }
}
