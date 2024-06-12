using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Services;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class OrderServiceTest
    {
        private OrderService _orderService;
        private Video _testVideo;
        private CartItem _testCartItem;
        private Cart _testCart;
        private Mock<ICartRepository> _cartRepositoryMock;
        private Mock<ICartItemsRepository> _cartItemsRepositoryMock;
        private Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IRentalRepository> _rentalRepositoryMock;
        private Mock<IPermanentRepository> _permanentRepositoryMock;
        private Mock<IVideoRepository> _videoRepositoryMock;
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private Mock<IDTOService> _dtoServiceMock;
        private Mock<IRefundRepository> _refundRepositoryMock;
        private Mock<IGeoLocationServices> _geoLocationServicesMock;
        private VideoStoreContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VideoStoreContext>()
               .UseInMemoryDatabase(databaseName: "db_test")
               .Options;

            _context = new VideoStoreContext(options);

            _context = new VideoStoreContext(options);
            _cartRepositoryMock = new Mock<ICartRepository>();
            _cartItemsRepositoryMock = new Mock<ICartItemsRepository>();
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _permanentRepositoryMock = new Mock<IPermanentRepository>();
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();
            _dtoServiceMock = new Mock<IDTOService>();
            _refundRepositoryMock = new Mock<IRefundRepository>();
            _geoLocationServicesMock = new Mock<IGeoLocationServices>();

            _orderService = new OrderService(
                _cartRepositoryMock.Object,
                _cartItemsRepositoryMock.Object,
                _orderItemRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _paymentRepositoryMock.Object,
                _tokenServiceMock.Object,
                _addressRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _rentalRepositoryMock.Object,
                _permanentRepositoryMock.Object,
                _videoRepositoryMock.Object,
                _inventoryRepositoryMock.Object,
                _context,
                _dtoServiceMock.Object,
                _refundRepositoryMock.Object,
                _geoLocationServicesMock.Object
            );
            _testVideo = new Video
            {
                Id = 1,
                Genre = VideoStoreManagementApi.Models.Enums.Genre.Action,
                Director = "Test Director",
                Price = 10.22f,
                ReleaseDate = DateTime.Now.AddDays(-2),
                Description = "Test Description",
                Tittle = "Test Title",
            };
            // Setup test cart item
            _testCartItem = new CartItem
            {
                Id = 1,
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.22f,

            };

            // Setup test cart
            _testCart = new Cart
            {
                Id = 1,
                CustomerId = 1,
                TotalPrice = 10.22f,
            };
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(1); // Mock token service
            _cartRepositoryMock.Setup(c => c.GetByUserId(It.IsAny<int>())).ReturnsAsync(_testCart); // Mock cart repository
            _cartItemsRepositoryMock.Setup(ci => ci.GetCartItemsWithCartId(It.IsAny<int>())).ReturnsAsync(new List<CartItem> { _testCartItem }); // Mock cart items repository
            _addressRepositoryMock.Setup(a => a.CheckAddressIsOfUser(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true); // Mock address repository
            _customerRepositoryMock.Setup(c => c.GetMembershipType(It.IsAny<int>())).ReturnsAsync(MembershipType.Basic); // Mock customer repository
            _orderRepositoryMock.Setup(o => o.Add(It.IsAny<Order>())).ReturnsAsync(new Order { Id = 1, CustomerId = 1, DeliveryAddressId = 1, ExpectedDeliveryDate = DateTime.Now.AddDays(7), OrderedDate = DateTime.Now, OrderStatus = OrderStatus.Shipped, PaymentType = PaymentType.UPI }); // Mock order repository
            _paymentRepositoryMock.Setup(p => p.Add(It.IsAny<Payment>())).ReturnsAsync(new Payment { Id = 1, TransactionId = 12345678, Amount = _testCart.TotalPrice, OrderId = 1, PaymentDate = DateTime.Now, PaymentSucess = true }); // Mock payment repository
            _orderRepositoryMock.Setup(o => o.Update(It.IsAny<Order>())).ReturnsAsync(new Order { Id = 1, PaymentDone = true }); // Mock order repository update
            _rentalRepositoryMock.Setup(r => r.Add(It.IsAny<Rental>())).ReturnsAsync(new Rental { Id = 1 }); // Mock rental repository
            _inventoryRepositoryMock.Setup(i => i.GetById(It.IsAny<int>())).ReturnsAsync(new Inventory { VideoId = 1, Stock = 10 }); // Mock inventory repository
            _orderItemRepositoryMock.Setup(o => o.Add(It.IsAny<OrderItem>())).ReturnsAsync(new OrderItem { Id = 1 }); // Mock order item repository
            _cartItemsRepositoryMock.Setup(ci => ci.Delete(It.IsAny<int>())).ReturnsAsync(true); // Mock cart items repository delete
            _cartRepositoryMock.Setup(c => c.Delete(It.IsAny<int>())).ReturnsAsync(true); // Mock cart repository delete
            _addressRepositoryMock.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(new Address
            {
                Id = 1,
                Area = "test",
                PrimaryAdress = true,
                City = "test",
                State = "test",
                CustomerId = 1,
                Zipcode = 12345
            }); // Mock address repository
            _dtoServiceMock.Setup(dto => dto.MapOrderToOrderDTO(
           It.IsAny<Order>(), It.IsAny<IList<OrderItem>>(), It.IsAny<Address>(), It.IsAny<Payment?>()))
           .Returns(new OrderDTO
           {
               Amount = _testCart.TotalPrice,
               CustomerId = 1,
               TransactionId = 12345678,
               PaymentDone = true,
               PaymentType = PaymentType.UPI,
               RentalOrPermanent = "Rental",
               OrderStatus = OrderStatus.Shipped,
               PaymentDate = DateTime.UtcNow,
               OrderedDate = DateTime.UtcNow,
               ExpectedDeliveryDate = DateTime.Now.AddDays(7),
               OrderId = 1,
           });
            _inventoryRepositoryMock.Setup(i => i.GetQty(It.IsAny<int>())).ReturnsAsync(10);


        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]
        public async Task GetOrdersOfUserPassTest()
        {
            // Arrange
            int userId = 1;
            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerId = userId, OrderItems = new List<OrderItem>(), DeliveryAddress = new Address() },
                new Order { Id = 2, CustomerId = userId, OrderItems = new List<OrderItem>(), DeliveryAddress = new Address() }
            };
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(userId);
            _orderRepositoryMock.Setup(o => o.GetOrdersByUid(userId)).ReturnsAsync(orders);
            _paymentRepositoryMock.Setup(p => p.GetPaymentByOrderId(It.IsAny<int>())).ReturnsAsync(new Payment());
            _dtoServiceMock.Setup(dto => dto.MapOrderToOrderDTO(It.IsAny<Order>(), It.IsAny<List<OrderItem>>(), It.IsAny<Address>(), It.IsAny<Payment?>()))
                           .Returns<Order, List<OrderItem>, Address, Payment?>((order, orderItems, address, payment) =>
                           new OrderDTO { OrderId = order.Id });

            // Act
            var result = await _orderService.GetOrdersOfUser();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IList<OrderDTO>>(result);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetOrdersOfUserFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(() => null);

            // Act && Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _orderService.GetOrdersOfUser());
        }
        [Test]
        public void GetOrdersOfUserFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int userId = 1;
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(userId);
            _orderRepositoryMock.Setup(o => o.GetOrdersByUid(userId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.GetOrdersOfUser());
        }
        [Test]
        public async Task GetOrderByIdPassTest()
        {
            // Arrange
            int orderId = 1;
            int userId = 1;
            var order = new Order { Id = orderId, CustomerId = userId, OrderItems = new List<OrderItem>(), DeliveryAddress = new Address() };
            var payment = new Payment();
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(userId);
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);
            _paymentRepositoryMock.Setup(p => p.GetPaymentByOrderId(orderId)).ReturnsAsync(payment);
            _dtoServiceMock.Setup(dto => dto.MapOrderToOrderDTO(order, It.IsAny<List<OrderItem>>(), order.DeliveryAddress, payment))
                           .Returns(new OrderDTO { OrderId = orderId });

            // Act
            var result = await _orderService.GetOrderById(orderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OrderDTO>(result);
            Assert.That(result.OrderId, Is.EqualTo(orderId));
        }

        [Test]
        public void GetOrderByIdFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            int orderId = 1;
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(() => null);

            // Act + Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _orderService.GetOrderById(orderId));
        }
        [Test]
        public void GetOrderByIdFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int orderId = 1;
            int userId = 1;
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(userId);
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.GetOrderById(orderId));
        }
        [Test]
        public void GetOrderByIdFailTest_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            int orderId = 1;
            int userId = 1;
            var order = new Order { Id = orderId, CustomerId = 2 };
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(userId);
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);

            // Act + Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _orderService.GetOrderById(orderId));
        }
         [Test]
        public async Task ChangeOrderStatusForAdminPassTest()
        {
            // Arrange
            int orderId = 1;
            var initialStatus = OrderStatus.Transit;
            var newStatus = OrderStatus.Shipped;
            var order = new Order { Id = orderId, OrderStatus = initialStatus };
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.ChangeOrderStatusForAdmin(orderId, newStatus);

            // Assert
            Assert.That(result, Is.EqualTo(newStatus));
            Assert.That(order.OrderStatus, Is.EqualTo(newStatus));
            _orderRepositoryMock.Verify(o => o.Update(order), Times.Once);
        }

        [Test]
        public void ChangeOrderStatusForAdminFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int orderId = 1;
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.ChangeOrderStatusForAdmin(orderId, OrderStatus.Shipped));
            _orderRepositoryMock.Verify(o => o.Update(It.IsAny<Order>()), Times.Never);
        }
        [Test]
        public async Task MarkPaymentDoneCodForAdminPassTest()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId };
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.MarkPaymentDoneCodForAdmin(orderId);

            // Assert
            Assert.That(result.Message, Is.EqualTo("Payment Changed Sucessfully"));
            Assert.IsTrue(order.PaymentDone);
        }

        [Test]
        public void MarkPaymentDoneCodForAdminFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int orderId = 1;
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.MarkPaymentDoneCodForAdmin(orderId));
        }
        [Test]
        public async Task CancelOrderPassTest_CancelsOrder()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId, CustomerId = 1, OrderStatus = OrderStatus.Shipped, PaymentType = PaymentType.COD };
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(1);

            // Act
            var result = await _orderService.CancelOrder(orderId);

            // Assert
            Assert.That(result.Message, Is.EqualTo("Order Cancelled Sucessfully"));
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Canceled));
            _orderRepositoryMock.Verify(r => r.Update(order), Times.Once);
        }
        [Test]
        public void CancelOrderFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int orderId = 1;
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.CancelOrder(orderId));
        }
        [Test]
        public void CancelOrderFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId, CustomerId = 2 }; // Different customer ID
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(() => null);

            // Act + Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _orderService.CancelOrder(orderId));
        }
        [Test]
        public async Task CheckRefundStatusPassTest_ReturnsRefundDTO()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId, CustomerId = 1 };
            var refund = new Refund { OrderId = orderId };
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(1);
            _refundRepositoryMock.Setup(r => r.GetRefundByOrderId(orderId)).ReturnsAsync(refund);
            _dtoServiceMock.Setup(d => d.MapRefundToRefundDTO(refund)).Returns(new RefundDTO { /* mock DTO properties */ });

            // Act
            var result = await _orderService.CheckRefundStatus(orderId);

            // Assert
            Assert.IsNotNull(result);
            
        }

        [Test]
        public void CheckRefundStatusFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId, CustomerId = 2 }; // Different customer ID
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);
            _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(() => null);

            // Act + Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _orderService.CheckRefundStatus(orderId));
        }
        [Test]
        public async Task IssuseRefundForAdminPassTest()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId };
            var refund = new Refund { OrderId = orderId };
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(order);
            _refundRepositoryMock.Setup(r => r.GetRefundByOrderId(orderId)).ReturnsAsync(refund);

            // Act
            var result = await _orderService.IssuseRefundForAdmin(orderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Message, Is.EqualTo("Changed Refund Status Sucesfully"));
            Assert.That(refund.Status, Is.EqualTo(RefundStatus.Refunded));
            _refundRepositoryMock.Verify(r => r.Update(refund), Times.Once);
        }

        [Test]
        public void IssuseRefundForAdminFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int orderId = 1;
            _orderRepositoryMock.Setup(o => o.GetById(orderId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.IssuseRefundForAdmin(orderId));
        }
        [Test]
        public async Task ViewAllRefundsForAdminPassTest()
        {
            // Arrange
            var refunds = new List<Refund> { new Refund(), new Refund() };
            _refundRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(refunds);

            // Act
            var result = await _orderService.ViewAllRefundsForAdmin();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IList<Refund>>(result);
            Assert.That(result.Count, Is.EqualTo(refunds.Count));
            
        }
        [Test]
        public async Task ViewAllOrdersAreBasedOnStatusdForAdminPassTest()
        {
            // Arrange
            var orderStatus = OrderStatus.Shipped;
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderStatus = OrderStatus.Shipped },
                new Order { Id = 2, OrderStatus = OrderStatus.OutForDelivery },
                new Order { Id = 3, OrderStatus = OrderStatus.Shipped }
            };
            _orderRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.ViewAllOrdersAreBasedOnStatusdForAdmin(orderStatus);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ToList().Count, Is.EqualTo(2));
            Assert.IsTrue(result.All(o => o.OrderStatus == orderStatus));
        }

        [Test]
        public async Task GetOrderbyIdForAdminPassTest()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { Id = orderId };
            _orderRepositoryMock.Setup(r => r.GetById(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderbyIdForAdmin(orderId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(orderId));
        }

        [Test]
        public void GetOrderbyIdForAdminFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            int orderId = 1;
            _orderRepositoryMock.Setup(r => r.GetById(orderId)).ReturnsAsync(() => null);

            // Act + Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _orderService.GetOrderbyIdForAdmin(orderId));
        }
        [Test]
        public async Task GetAllOrdersForAdminPass()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var totalOrders = 25; // Total orders available in the repository
            var orders = Enumerable.Range(1, totalOrders).Select(i => new Order { Id = i }).ToList();
            _orderRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllOrdersForAdmin(pageNumber, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(pageSize)); // Check if correct page size is returned
            Assert.That(result.First().Id, Is.EqualTo(1)); // Check if the first order in the page is correct
        }

        [Test]
        public void GetAllOrdersForAdmin_WithInvalidPageNumber_ThrowsArgumentException()
        {
            // Arrange
            var pageNumber = -1; 
            var pageSize = 10;

            // Act + Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.GetAllOrdersForAdmin(pageNumber, pageSize));
        }

        [Test]
        public void GetAllOrdersForAdmin_WithInvalidPageSize_ThrowsArgumentException()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 0; // Invalid page size

            // Act + Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.GetAllOrdersForAdmin(pageNumber, pageSize));
        }
        //[Test]
        //public async Task MakePayment_ShouldCommitTransaction_WhenPaymentIsSuccessful()
        //{
        //    // Arrange
        //    var uid = 1;
        //    var addressId = 1;
        //    var paymentType = PaymentType.UPI;
        //    var membershipType = MembershipType.Basic;

        //    var cart = new Cart { Id = 1, CustomerId = uid, TotalPrice = 100 };
        //    var cartItems = new List<CartItem>
        //    {
        //        new CartItem { Id = 1, CartId = 1, VideoId = 1, Qty = 2, Price = 50 },
        //        new CartItem { Id = 2, CartId = 1, VideoId = 2, Qty = 1, Price = 50 }
        //    };

        //    _tokenServiceMock.Setup(t => t.GetUidFromToken()).Returns(uid);
        //    _cartRepositoryMock.Setup(c => c.GetByUserId(uid)).ReturnsAsync(cart);
        //    _cartItemsRepositoryMock.Setup(c => c.GetCartItemsWithCartId(cart.Id)).ReturnsAsync(cartItems);
        //    _addressRepositoryMock.Setup(a => a.CheckAddressIsOfUser(uid, addressId)).ReturnsAsync(true);
        //    _customerRepositoryMock.Setup(c => c.GetMembershipType(uid)).ReturnsAsync(membershipType);
        //    _inventoryRepositoryMock.Setup(i => i.GetQty(It.IsAny<int>())).ReturnsAsync(5);
        //    _dtoServiceMock.Setup(d => d.MapOrderToOrderDTO(It.IsAny<Order>(), It.IsAny<List<OrderItem>>(), It.IsAny<Address>(), It.IsAny<Payment>())).Returns(new OrderDTO());

        //    // Act
        //    var result = await _orderService.MakePayment(paymentType, addressId);

        //    // Assert
        //    _transactionMock.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}
    }
}
