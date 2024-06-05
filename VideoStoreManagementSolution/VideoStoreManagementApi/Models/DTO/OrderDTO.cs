using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VideoStoreManagementApi.Models.Enums;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models.DTO
{
    public class OrderDTO
    {
        
      
        public int OrderId { get; set; }
        public float Amount { get; set; }
        public int TransactionId { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int CustomerId { get; set; }
      
        public DateTime OrderedDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime? RealDeliveredDate { get; set; }

        public string RentalOrPermanent { get; set; }
        public bool PaymentDone { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Address DeliveryAddress { get; set; }
        public IList<OrderItem> Items { get; set; }
     
    }
}
