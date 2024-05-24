using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models
{
    public class Permanent
    {
        public int Id { get; set; } 
        public int OrderId {  get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }    
        public int TotalQty { get; set; }
        public float TotalPrice { get; set; }

    }
}
