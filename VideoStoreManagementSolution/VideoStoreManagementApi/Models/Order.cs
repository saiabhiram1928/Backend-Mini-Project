using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime ExpectedDeliveryDate {  get; set; } 
        public DateTime RealDeliveredDate { get; set; }
        public int DeliveryAddressId { get; set; }
        [ForeignKey(nameof(DeliveryAddressId))]
        public Address DeliveryAddress { get; set; }
        public MembershipType RentalOrEnum { get; set; }
        public bool PaymentDone { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; } 
        public ICollection<Payment> Payments { get; set; }

        public Permanent Permanent { get; set; }
        public Rental Rental { get; set; }

    }
}
