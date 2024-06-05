using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class OrderRepositoryTest
    {
        private VideoStoreContext _context;
        private IOrderRepository _orderRepository;
        private IAddressRepository _addressRepository;
        Address address;

        [SetUp]
        public async Task SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _orderRepository = new OrderRepository(_context);
            _addressRepository = new AddressRepsitory(_context);
             address = new Address() { Area = "test", City = "test", CustomerId = 1, PrimaryAdress = true, Zipcode = 507001, State = "test" };
            address = await _addressRepository.Add(address);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddOrderPassTest()
        {
            var order = new Order
            {
                CustomerId = 1,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = address.Id,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Transit,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
              
            };

            var addedOrder = await _orderRepository.Add(order);

            Assert.IsNotNull(addedOrder);
            Assert.That(addedOrder.CustomerId, Is.EqualTo(order.CustomerId));
        }

        [Test]
        public async Task AddOrderFailTest()
        {
            var order = new Order
            {
                CustomerId = 1,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = address.Id,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Transit,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
                
            };
            await _orderRepository.Add(order);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _orderRepository.Add(order);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdateOrderPassTest()
        {
            var order = new Order
            {
                CustomerId = 1,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = 1,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Transit,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
              
            };
            order = await _orderRepository.Add(order);

            order.TotalAmount = 150.00f;
            var updatedOrder = await _orderRepository.Update(order);

            Assert.IsNotNull(updatedOrder);
            Assert.That(updatedOrder.TotalAmount, Is.EqualTo(150.00f));
        }

        [Test]
        public async Task UpdateOrderFailTest()
        {
            var order = new Order
            {
                CustomerId = 1,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = address.Id,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Transit,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
           
            };
            order = await _orderRepository.Add(order);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                order.Id = 100;
                await _orderRepository.Update(order);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeleteOrderPassTest()
        {
            var order = new Order
            {
                CustomerId = 1,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = address.Id,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Delivered,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
              
            };
            order = await _orderRepository.Add(order);

            var result = await _orderRepository.Delete(order.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteOrderFailTest()
        {
            var result = await _orderRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetOrderByIdPassTest()
        {
            var order = new Order
            {
                CustomerId = 1,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = 1,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.OutForDelivery,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
               
            };
            order = await _orderRepository.Add(order);
            Console.WriteLine(order.Id);

            var result = await _orderRepository.GetById(order.Id);

            Assert.IsNotNull(result);
            Assert.That(result.TotalAmount, Is.EqualTo(order.TotalAmount));
        }

        [Test]
        public async Task GetOrderByIdFailTest()
        {
            var result = await _orderRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllOrdersPassTest()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = 1,
                    OrderedDate = DateTime.Now,
                    ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                    DeliveryAddressId = 1,
                    RentalOrPermanent = "Rental",
                    PaymentDone = true,
                    OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Transit,
                    PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                    TotalAmount = 100.00f,
                    OrderItems = new List<OrderItem>(),
                    Payments = new List<Payment>()
                },
                new Order
                {
                    CustomerId = 2,
                    OrderedDate = DateTime.Now,
                    ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                    DeliveryAddressId = 2,
                    RentalOrPermanent = "Permanent",
                    PaymentDone = false,
                    OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.Delivered,
                    PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                    TotalAmount = 200.00f,
                    OrderItems = new List<OrderItem>(),
                    Payments = new List<Payment>()
                }
            };

           await _orderRepository.Add(orders[0]);
            await _orderRepository.Add(orders[1]);

            var result = await _orderRepository.GetAll();

            Assert.IsNotNull(result);
          
        }

        [Test]
        public async Task GetAllOrdersFailTest()
        {
            var result = await _orderRepository.GetAll();

            Assert.IsEmpty(result);
        }



        [Test]
        public async Task GetOrdersByUidPassTest()
        {
            var customerId = 1;
       
            var orders = new List<Order>
        {
            new Order
            {
                CustomerId = customerId,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = address.Id,
                RentalOrPermanent = "Rental",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.OutForDelivery,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 100.00f,
            },
            new Order
            {
                CustomerId = customerId,
                OrderedDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(7),
                DeliveryAddressId = address.Id,
                RentalOrPermanent = "Permanent",
                PaymentDone = true,
                OrderStatus = VideoStoreManagementApi.Models.Enums.OrderStatus.OutForDelivery,
                PaymentType = VideoStoreManagementApi.Models.Enums.PaymentType.UPI,
                TotalAmount = 200.00f    
            }
        };

            orders[0] = await _orderRepository.Add(orders[0]);
            orders[1] = await _orderRepository.Add(orders[1]);

            var result = await _orderRepository.GetOrdersByUid(customerId);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].CustomerId, Is.EqualTo(customerId));
            Assert.That(result[1].CustomerId, Is.EqualTo(customerId));
        }

        [Test]
        public async Task GetOrdersByUidFailTest()
        {
            var result = await _orderRepository.GetOrdersByUid(999);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }




    }
}
