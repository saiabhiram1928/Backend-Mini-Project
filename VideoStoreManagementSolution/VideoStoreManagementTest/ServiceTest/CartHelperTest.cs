using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services.Helpers;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class CartHelperTest
    {
        [Test]
        public void CreateCart_ReturnsCartWithCustomerIdSet()
        {
            // Arrange
            int customerId = 1;
            var cartHelpers = new CartHelpers();

            // Act
            var cart = cartHelpers.CreateCart(customerId);

            // Assert
            Assert.IsNotNull(cart);
            Assert.That(cart.CustomerId, Is.EqualTo(customerId));
        }

        [Test]
        public void CreateCartItem_ReturnsCartItemWithVideoIdAndCartIdSet()
        {
            // Arrange
            int videoId = 1;
            int cartId = 2;
            var cartHelpers = new CartHelpers();

            // Act
            var cartItem = cartHelpers.CreateCartItem(videoId, cartId);

            // Assert
            Assert.IsNotNull(cartItem);
            Assert.That(cartItem.VideoId, Is.EqualTo(videoId));
            Assert.That(cartItem.CartId, Is.EqualTo(cartId));
        }

        [Test]
        public void CalculateTotalPrice_ReturnsCorrectTotalPrice()
        {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem { Price = 10 },
                new CartItem { Price = 20 },
                new CartItem { Price = 30 }
            };
            var cartHelpers = new CartHelpers();

            // Act
            var totalPrice = cartHelpers.CalculateTotalPrice(cartItems);

            // Assert
            Assert.AreEqual(60, totalPrice);
        }
    }

}
