using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services.Helpers;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class OrderHelperTest
    {
        [Test]
        public async Task CreateOrder_ReturnsOrderWithExpectedProperties()
        {
            // Arrange
            int customerId = 1;
            int addressId = 2;
            var paymentType = PaymentType.UPI;
            var membershipType = MembershipType.Basic;

            // Act
            var order = await OrderHelper.CreateOrder(customerId, addressId, paymentType, membershipType);

            // Assert
            Assert.IsNotNull(order);
            Assert.That(order.CustomerId, Is.EqualTo(customerId));
            Assert.That(order.DeliveryAddressId, Is.EqualTo(addressId));
            Assert.That(order.PaymentType, Is.EqualTo(paymentType));
            Assert.That(order.RentalOrPermanent, Is.EqualTo(membershipType == MembershipType.Basic ? "Rental" : "Permanent"));
            Assert.IsFalse(order.PaymentDone);
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Shipped));
            Assert.IsTrue(order.OrderedDate <= DateTime.Now);
            Assert.IsTrue(order.ExpectedDeliveryDate <= DateTime.Now.AddDays(7));
        }

        [Test]
        public void GenerateTransactionId_ReturnsNonZeroId()
        {
            // Act
            int transactionId = OrderHelper.GenerateTransactionId();

            // Assert
            Assert.That(transactionId, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task SetExpectedDeliveryDate_CalculatesExpectedDeliveryDate()
        {
            // Arrange
            var mockGeoLocationService = new Mock<IGeoLocationServices>();
            mockGeoLocationService.Setup(x => x.GetDistanceAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(100000); // Assuming a distance of 100km

            var address = new Address
            {
                Area = "Some Area",
                City = "Some City",
                State = "Some State",
                Zipcode =  123456
            };

            // Act
            var expectedDeliveryDate = await OrderHelper.SetExceptedDeliveryDate(address, mockGeoLocationService.Object);

            // Assert
            Assert.IsTrue(expectedDeliveryDate > DateTime.Now);
        }
    }
}
