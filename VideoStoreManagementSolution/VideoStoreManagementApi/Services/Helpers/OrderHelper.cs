using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Services.Helpers
{
    public class OrderHelper
    {
        //public int Id { get; set; }
        //public int CustomerId { get; set; }
        //[ForeignKey(nameof(CustomerId))]
        //public Customer Customer { get; set; }
        //public DateTime OrderedDate { get; set; }
        //public DateTime ExpectedDeliveryDate { get; set; }
        //public DateTime? RealDeliveredDate { get; set; }
        //public int DeliveryAddressId { get; set; }
        //[ForeignKey(nameof(DeliveryAddressId))]
        //public Address DeliveryAddress { get; set; }
        //public string RentalOrPermanent { get; set; }
        //public bool PaymentDone { get; set; }

        //public ICollection<OrderItem> OrderItems { get; set; }
        //public ICollection<Payment> Payments { get; set; }

        //public Permanent Permanent { get; set; }
        //public Rental Rental { get; set; }
        public static Order CreateOrder(int customerId , int addressId, PaymentType paymentType,MembershipType membershipType)
        {
            Order order = new Order();
            order.CustomerId = customerId;
            order.OrderedDate = DateTime.Now;
            order.ExpectedDeliveryDate = DateTime.Now.AddDays(7);
            order.PaymentDone = false;
            order.DeliveryAddressId = addressId;
            order.PaymentType = paymentType;
            order.RentalOrPermanent = membershipType == MembershipType.Basic ? MembershipType.Basic.ToString() : MembershipType.Premium.ToString() ;
            order.DeliveryStatus = DeliveryStatus.Shipped;
            return order;
        }
        public static int GenerateTransactionId()
        {

            Guid guid = Guid.NewGuid();
            
            int transactionId = guid.GetHashCode();
            return transactionId;
        }
    }
}
