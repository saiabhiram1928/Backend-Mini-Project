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
    internal class OrderItemRepositoryTest
    {
        private VideoStoreContext _context;
        private IOrderItemRepository _orderItemRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _orderItemRepository = new OrderItemRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddOrderItem_PassTest()
        {
            var orderItem = new OrderItem
            {
                OrderId = 1,
                VideoId = 1,
                Qty = 2,
                Price = 20.00f
            };

            var addedOrderItem = await _orderItemRepository.Add(orderItem);

            Assert.IsNotNull(addedOrderItem);
            Assert.That(addedOrderItem.OrderId, Is.EqualTo(orderItem.OrderId));
        }

        [Test]
        public async Task AddOrderItem_FailTest()
        {
            var orderItem = new OrderItem
            {
                OrderId = 1,
                VideoId = 1,
                Qty = 2,
                Price = 20.00f
            };
            await _orderItemRepository.Add(orderItem);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _orderItemRepository.Add(orderItem);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdateOrderItem_PassTest()
        {
            var orderItem = new OrderItem
            {
                OrderId = 1,
                VideoId = 1,
                Qty = 2,
                Price = 20.00f
            };
            orderItem = await _orderItemRepository.Add(orderItem);

            orderItem.Price = 25.00f;
            var updatedOrderItem = await _orderItemRepository.Update(orderItem);

            Assert.IsNotNull(updatedOrderItem);
            Assert.That(updatedOrderItem.Price, Is.EqualTo(25.00f));
        }

        [Test]
        public async Task UpdateOrderItem_FailTest()
        {
            var orderItem = new OrderItem
            {
                OrderId = 1,
                VideoId = 1,
                Qty = 2,
                Price = 20.00f
            };
            orderItem = await _orderItemRepository.Add(orderItem);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                orderItem.Id = 100;
                await _orderItemRepository.Update(orderItem);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeleteOrderItem_PassTest()
        {
            var orderItem = new OrderItem
            {
                OrderId = 1,
                VideoId = 1,
                Qty = 2,
                Price = 20.00f
            };
            orderItem = await _orderItemRepository.Add(orderItem);

            var result = await _orderItemRepository.Delete(orderItem.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteOrderItem_FailTest()
        {
            var result = await _orderItemRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetOrderItemById_PassTest()
        {
            var orderItem = new OrderItem
            {
                OrderId = 1,
                VideoId = 1,
                Qty = 2,
                Price = 20.00f
            };
            orderItem = await _orderItemRepository.Add(orderItem);

            var result = await _orderItemRepository.GetById(orderItem.Id);

            Assert.IsNotNull(result);
            Assert.That(result.OrderId, Is.EqualTo(orderItem.OrderId));
        }

        [Test]
        public async Task GetOrderItemById_FailTest()
        {
            var result = await _orderItemRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetOrderItemsByOrderId_PassTest()
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    OrderId = 1,
                    VideoId = 1,
                    Qty = 2,
                    Price = 20.00f
                },
                new OrderItem
                {
                    OrderId = 1,
                    VideoId = 2,
                    Qty = 1,
                    Price = 15.00f
                }
            };

            orderItems[0] = await _orderItemRepository.Add(orderItems[0]);
            orderItems[1] = await _orderItemRepository.Add(orderItems[1]);
            var result = await _orderItemRepository.GetOrderItemsbyOrderId(orderItems[0].Id);

            Assert.IsNotNull(result);
          
        }

        [Test]
        public async Task GetOrderItemsByOrderId_FailTest()
        {
            var result = await _orderItemRepository.GetOrderItemsbyOrderId(100);

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllOrderItems_PassTest()
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    OrderId = 1,
                    VideoId = 1,
                    Qty = 2,
                    Price = 20.00f
                },
                new OrderItem
                {
                    OrderId = 2,
                    VideoId = 2,
                    Qty = 1,
                    Price = 15.00f
                }
            };
            foreach (var orderItem in orderItems)
            {
                await _orderItemRepository.Add(orderItem);
            }

            var result = await _orderItemRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(orderItems, result);
        }

        [Test]
        public async Task GetAllOrderItems_FailTest()
        {
            var result = await _orderItemRepository.GetAll();

            Assert.IsEmpty(result);
        }
    }
}
