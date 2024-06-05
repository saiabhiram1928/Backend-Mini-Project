using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [JsonIgnore]
        public Customer Customer { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime ExpectedDeliveryDate {  get; set; } 
        public DateTime? RealDeliveredDate { get; set; }
        public int DeliveryAddressId { get; set; }
        [ForeignKey(nameof(DeliveryAddressId))]
        public Address DeliveryAddress { get; set; }
        public string RentalOrPermanent { get; set; }
        public bool PaymentDone { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public float TotalAmount { get; set; }  
        public ICollection<OrderItem> OrderItems { get; set; } 
        public ICollection<Payment> Payments { get; set; }
        public Permanent Permanent { get; set; }
        public Rental Rental { get; set; }
        public Refund Refund { get; set; }


    }
}
