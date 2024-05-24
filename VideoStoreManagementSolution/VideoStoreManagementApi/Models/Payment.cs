using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models
{
    public class Payment
    {
        [Key]
        public int TransactionId { get; set; }  
        public float Amount { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public DateTime PaymentDate { get; set; }   
        public bool PaymentSucess { get; set; }
    }
}
