using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services;
using VideoStoreManagementApi.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class DTOServiceTest
    {
        private DTOService _dtoService;

        [SetUp]
        public void SetUp()
        {
            _dtoService = new DTOService();
        }
        [Test]
        public void MapOrderToOrderDTO_WithPayment_ReturnsCorrectOrderDTO()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = 101,
                OrderStatus = OrderStatus.Shipped,
             
            };
            var orderItems = new List<OrderItem>();
            var address = new Address();
            var payment = new Payment
            {
                TransactionId = 12345678,
                PaymentDate = DateTime.Now,
                 Amount =100f,
                 Id =1,
                 OrderId = order.Id,
                 PaymentSucess = true
                
            };

            // Act
            var result = _dtoService.MapOrderToOrderDTO(order, orderItems, address, payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.OrderId, Is.EqualTo(order.Id));
            Assert.That(result.CustomerId, Is.EqualTo(order.CustomerId));
            Assert.That(result.OrderStatus, Is.EqualTo(order.OrderStatus));
            Assert.That(result.TransactionId, Is.EqualTo(payment.TransactionId));
            Assert.That(result.PaymentDate, Is.EqualTo(payment.PaymentDate));
        }

        [Test]
        public void MapOrderToOrderDTO_WithoutPayment_ReturnsCorrectOrderDTO()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = 101,
                OrderStatus = OrderStatus.Shipped,
                
            };
            var orderItems = new List<OrderItem>();
            var address = new Address();

            // Act
            var result = _dtoService.MapOrderToOrderDTO(order, orderItems, address, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.OrderId, Is.EqualTo(order.Id));
            Assert.That(result.CustomerId, Is.EqualTo(order.CustomerId));
            Assert.That(result.OrderStatus, Is.EqualTo(order.OrderStatus));
            Assert.That(result.TransactionId, Is.EqualTo(0));
            Assert.IsNull(result.PaymentDate);
        }

        [Test]
        public void MapAddressDTOToAddress_ReturnsMappedAddress()
        {
            // Arrange
            var addressDTO = new AddressDTO
            {
                Area = "Area",
                City = "City",
                PrimaryAdress = true,
                Zipcode = 12345,
                State = "State"
            };
            var address = new Address();

            // Act
            var result = _dtoService.MapAddressDTOToAddress(addressDTO, address);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Area, Is.EqualTo(addressDTO.Area));
            Assert.That(result.City, Is.EqualTo(addressDTO.City));
            Assert.That(result.PrimaryAdress, Is.EqualTo(addressDTO.PrimaryAdress));
            Assert.That(result.Zipcode, Is.EqualTo(addressDTO.Zipcode));
            Assert.That(result.State, Is.EqualTo(addressDTO.State));
        }
        [Test]  
        public void MapAddressRegisterDTOTOAddressDTO_ValidAddressRegisterDTO_ReturnsMappedAddressDTO()
        {
            // Arrange
            var addressRegisterDTO = new AddressRegisterDTO
            {
                Area = "Area",
                City = "City",
                PrimaryAdress = true,
                Zipcode = 12345,
                State = "State"
            };

            // Act
            var result = _dtoService.MapAddressRegisterDTOTOAddressDTO(addressRegisterDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Area, Is.EqualTo(addressRegisterDTO.Area));
            Assert.That(result.City, Is.EqualTo(addressRegisterDTO.City));
            Assert.That(result.PrimaryAdress, Is.EqualTo(addressRegisterDTO.PrimaryAdress));
            Assert.That(result.Zipcode, Is.EqualTo(addressRegisterDTO.Zipcode));
            Assert.That(result.State, Is.EqualTo(addressRegisterDTO.State));
        }
        [Test]
        public void MapAddressToAddressDTO_ValidAddress_ReturnsMappedAddressDTO()
        {
            // Arrange
            var address = new Address
            {
                Area = "Area",
                City = "City",
                PrimaryAdress = true,
                Zipcode = 1234,
                Id = 1,
                State = "State"
            };

            // Act
            var result = _dtoService.MapAddressToAddressDTO(address);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Area, Is.EqualTo(address.Area));
            Assert.That(result.City, Is.EqualTo(address.City));
            Assert.That(result.PrimaryAdress, Is.EqualTo(address.PrimaryAdress));
            Assert.That(result.Zipcode, Is.EqualTo(address.Zipcode));
            Assert.That(result.Id, Is.EqualTo(address.Id));
            Assert.That(result.State, Is.EqualTo(address.State));
        }

        [Test]
        public void MapUserRegisterDTOToCustomer_ValidUserRegisterDTOAndUser_ReturnsMappedCustomer()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                // Fill with required properties
            };
            var user = new User
            {
                // Fill with required properties
            };

            // Act
            var result = _dtoService.MapUserRegisterDTOToCustomer(userRegisterDTO, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.MembershipType, Is.EqualTo(MembershipType.Basic));
            Assert.That(result.Uid, Is.EqualTo(user.Uid));
        }

        [Test]
        public void MapUserRegisterDTOToUser_ValidUserRegisterDTO_ReturnsMappedUser()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = "test@example.com",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe"
            };
            var salt = new byte[] {  };
            var passwd = new byte[] { };

            // Act
            var result = _dtoService.MapUserRegisterDTOToUser(userRegisterDTO, salt, passwd);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.FirstName, Is.EqualTo(userRegisterDTO.FirstName));
            Assert.That(result.LastName, Is.EqualTo(userRegisterDTO.LastName));
            Assert.That(result.Password, Is.EqualTo(passwd));
            Assert.That(result.Salt, Is.EqualTo(salt));
            Assert.IsTrue(result.Verified);
            Assert.That(result.Email, Is.EqualTo(userRegisterDTO.Email));
            Assert.That(result.Role, Is.EqualTo(Role.Customer));
        }
        [Test]
        public void MapUserToUserReturnDTO_ReturnsCorrectDTO()
        {
            // Arrange
            var user = new Customer
            {
                User = new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Verified = true,
                    Role = Role.Customer
                },
                MembershipType = MembershipType.Premium
            };
            string token = "dummyToken";

            // Act
            var result = _dtoService.MapUserToUserReturnDTO(user, token);

            // Assert
            Assert.That(result.FirstName, Is.EqualTo(user.User.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.User.LastName));
            Assert.That(result.Email, Is.EqualTo(user.User.Email));
            Assert.That(result.Verified, Is.EqualTo(user.User.Verified));
            Assert.That(result.MembershipType, Is.EqualTo(user.MembershipType));
            Assert.That(result.Token, Is.EqualTo(token));
            Assert.That(result.Role, Is.EqualTo(user.User.Role));
        }

        [Test]
        public void MapVideoRegisterDTOToVideo_ReturnsCorrectVideo()
        {
            // Arrange
            var videoRegisterDTO = new VideoRegisterDTO
            {
                Tittle = "Test Video",
                Price = 9.99f,
                Description = "Description of test video",
                Director = "John Doe",
                Genre = Genre.Action,
                ReleaseDate = new DateTime(2022, 1, 1)
            };

            // Act
            var result = _dtoService.MapVideoRegisterDTOToVideo(videoRegisterDTO);

            // Assert
            Assert.That(result.Tittle, Is.EqualTo(videoRegisterDTO.Tittle));
            Assert.That(result.Price, Is.EqualTo(videoRegisterDTO.Price));
            Assert.That(result.Description, Is.EqualTo(videoRegisterDTO.Description));
            Assert.That(result.Director, Is.EqualTo(videoRegisterDTO.Director)  );
            Assert.That(result.Genre, Is.EqualTo(videoRegisterDTO.Genre));
            Assert.That(result.ReleaseDate, Is.EqualTo(videoRegisterDTO.ReleaseDate));
        }
        [Test]
        public void MapCartItemToCartItemDTO_ReturnsCorrectCartItemDTO()
        {
            // Arrange
            var cartItem = new CartItem
            {
                Id = 1,
                CartId = 101,
                VideoId = 201,
                Qty = 2,
                Price = 50.0f,
                Video = new Video { Id = 201, Tittle = "Test Video" }
            };

            // Act
            var result = _dtoService.MapCartItemToCartItemDTO(cartItem);

            // Assert
            Assert.That(result.CartId, Is.EqualTo(cartItem.CartId));
            Assert.That(result.CartItemId, Is.EqualTo(cartItem.Id));
            Assert.That(result.VideoId, Is.EqualTo(cartItem.VideoId));
            Assert.That(result.VideoTittle, Is.EqualTo(cartItem.Video.Tittle));
            Assert.That(result.Qty, Is.EqualTo(cartItem.Qty));
            Assert.That(result.Price, Is.EqualTo(cartItem.Price));
        }
        [Test]
        public void MapCartToCartDTO_ReturnsCorrectCartDTO()
        {
            // Arrange
            var cart = new Cart
            {
                Id = 1,
                CustomerId = 101,
                TotalPrice = 50.0f
            };

            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    CartId = cart.Id,
                    VideoId = 201,
                    Qty = 2,
                    Price = 20.0f,
                    Video = new Video { Id = 201, Tittle = "Test Video 1" }
                },
                new CartItem
                {
                    Id = 2,
                    CartId = cart.Id,
                    VideoId = 202,
                    Qty = 1,
                    Price = 10.0f,
                    Video = new Video { Id = 202, Tittle = "Test Video 2" }
                }
            };

            // Act
            var result = _dtoService.MapCartToCartDTO(cart, cartItems);

            // Assert
            Assert.That(result.cartId, Is.EqualTo(cart.Id));
            Assert.That(result.customerId, Is.EqualTo(cart.CustomerId));
            Assert.That(result.TotalPrice, Is.EqualTo(cart.TotalPrice));

            Assert.That(result.CartItems.Count, Is.EqualTo(cartItems.Count));
            for (int i = 0; i < cartItems.Count; i++)
            {
                Assert.That(result.CartItems[i].CartItemId, Is.EqualTo(cartItems[i].Id));
                Assert.That(result.CartItems[i].VideoId, Is.EqualTo(cartItems[i].VideoId));
                Assert.That(result.CartItems[i].Qty, Is.EqualTo(cartItems[i].Qty));
                Assert.That(result.CartItems[i].Price, Is.EqualTo(cartItems[i].Price));
                Assert.That(result.CartItems[i].VideoTittle, Is.EqualTo(cartItems[i].Video.Tittle));
            }
        }
        [Test]
        public void MapVideoToVideoDTO_ReturnsCorrectVideoDTO()
        {
            // Arrange
            var video = new Video
            {
                Id = 1,
                Description = "Test Description",
                Director = "Test Director",
                Genre = Genre.Action,
                Price = 10.0f,
                ReleaseDate = new DateTime(2022, 1, 1),
                Tittle = "Test Title"
            };
            int stock = 5;

            // Act
            var result = _dtoService.MapVideoToVideoDTO(video, stock);

            // Assert
            Assert.That(result.Id, Is.EqualTo(video.Id));
            Assert.That(result.Description, Is.EqualTo(video.Description));
            Assert.That(result.Director, Is.EqualTo(video.Director));
            Assert.That(result.Genre, Is.EqualTo(video.Genre));
            Assert.That(result.Price, Is.EqualTo(video.Price));
            Assert.That(result.ReleaseDate, Is.EqualTo(video.ReleaseDate));
            Assert.That(result.Stock, Is.EqualTo(stock));
            Assert.That(result.Tittle, Is.EqualTo(video.Tittle));
        }

        [Test]
        public void MapRefundToRefundDTO_ReturnsCorrectRefundDTO()
        {
            // Arrange
            var refund = new Refund
            {
                Id = 1,
                OrderId = 101,
                TranasactionId = 123456,
                Amount = 50.0f,
                Status = RefundStatus.Intiated
            };

            // Act
            var result = _dtoService.MapRefundToRefundDTO(refund);

            // Assert
            Assert.That(result.Id, Is.EqualTo(refund.Id));
            Assert.That(result.OrderId, Is.EqualTo(refund.OrderId));
            Assert.That(result.TranasactionId, Is.EqualTo(refund.TranasactionId));
            Assert.That(result.Amount, Is.EqualTo(refund.Amount));
            Assert.That(result.Status, Is.EqualTo(refund.Status));
        }
    }
}
