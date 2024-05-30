using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models
{
    public class Payment
    {


        [Key]
        public int Id { get; set; }
        public int TransactionId { get; set; }  
        public float Amount { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order Order { get; set; }
        public DateTime PaymentDate { get; set; }   
        public bool PaymentSucess { get; set; }
    }
}
