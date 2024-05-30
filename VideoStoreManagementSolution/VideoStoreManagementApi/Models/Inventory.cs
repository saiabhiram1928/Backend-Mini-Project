using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models
{
    public class Inventory
    {
        [Key] 
        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }
        public int Stock { get; set; }
        public DateTime LastUpdate { get; set; }   

    }
}
