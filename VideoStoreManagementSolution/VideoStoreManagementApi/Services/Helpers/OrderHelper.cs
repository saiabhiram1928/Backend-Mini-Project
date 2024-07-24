using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Services.Helpers
{
    public class OrderHelper
    {
        
        
        public static async Task<Order> CreateOrder(int customerId , int addressId, PaymentType paymentType,MembershipType membershipType)
        {
            Order order = new Order();
            order.CustomerId = customerId;
            order.OrderedDate = DateTime.Now;
            order.ExpectedDeliveryDate = DateTime.Now.AddDays(7);
            order.PaymentDone = false;
            order.DeliveryAddressId = addressId;
            order.PaymentType = paymentType;
            order.RentalOrPermanent = membershipType == MembershipType.Basic ? "Rental": "Permanent" ;
            order.OrderStatus = OrderStatus.Transit;
            return order;
        }
        public static int GenerateTransactionId()
        {

            Guid guid = Guid.NewGuid();
            
            int transactionId = guid.GetHashCode();
            return transactionId;
        }

        public static  async Task<DateTime> SetExceptedDeliveryDate(Address address , IGeoLocationServices geoLocationServices)
        {
            string source  = "zp center, Wyra Rd, Yedulapuram, Khammam, Telangana 507002";
            string destination = $"{address.Area} ,{address.City} , {address.State} ,  {address.Zipcode}";
            var distance =   await geoLocationServices.GetDistanceAsync(source, destination);
            const double deliverySpeedPerDay = 40 * 1000; 
            int daysRequired = (int)Math.Ceiling(distance / deliverySpeedPerDay);

            return DateTime.Now.AddDays(daysRequired);
        }

    }
}
