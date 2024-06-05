using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class RefundRepositoryTest
    {
        private VideoStoreContext _context;
        private IRefundRepository _refundRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _refundRepository = new RefundRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddRefund_PassTest()
        {
            var refund = new Refund
            {
                OrderId = 1,
                Amount = 50.00f,
                Status = VideoStoreManagementApi.Models.Enums.RefundStatus.Intiated,
                TranasactionId =123456,
            };

            var addedRefund = await _refundRepository.Add(refund);

            Assert.IsNotNull(addedRefund);
            Assert.That(addedRefund.OrderId, Is.EqualTo(refund.OrderId));
        }
        [Test]
        public async Task AddRefund_FailTest()
        {
            var refund = new Refund
            {
                OrderId = 1,
                Amount = 50.00f,
                Status = VideoStoreManagementApi.Models.Enums.RefundStatus.Intiated,
                TranasactionId = 123456,

            };
            await _refundRepository.Add(refund);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _refundRepository.Add(refund);
            });
            Assert.IsNotNull(ex);
        }
        [Test]
        public async Task UpdateRefund_PassTest()
        {
            var refund = new Refund
            {
                TranasactionId = 1,
                Amount = 50.00f,
                Status = RefundStatus.Refunded,
                OrderId = 1
            };
            refund = await _refundRepository.Add(refund);

            refund.Amount = 60.00f;
            var updatedRefund = await _refundRepository.Update(refund);

            Assert.IsNotNull(updatedRefund);
            Assert.That(updatedRefund.Amount, Is.EqualTo(60.00f));
        }

        [Test]
        public async Task UpdateRefund_FailTest()
        {
            var refund = new Refund
            {
                TranasactionId = 1,
                Amount = 50.00f,
                Status = RefundStatus.Intiated,
                OrderId = 1
            };
            refund = await _refundRepository.Add(refund);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                refund.Id = 100;
                await _refundRepository.Update(refund);
            });
            Assert.IsNotNull(ex);
        }
        [Test]
        public async Task DeleteRefund_PassTest()
        {
            var refund = new Refund
            {
                TranasactionId = 1,
                Amount = 50.00f,
                Status = RefundStatus.Intiated,
                OrderId = 1
            };
            refund = await _refundRepository.Add(refund);

            var result = await _refundRepository.Delete(refund.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteRefund_FailTest()
        {
            var result = await _refundRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetRefundById_PassTest()
        {
            var refund = new Refund
            {
                TranasactionId = 1,
                Amount = 50.00f,
                Status = RefundStatus.Intiated,
                OrderId = 1
            };
            refund = await _refundRepository.Add(refund);

            var result = await _refundRepository.GetById(refund.Id);

            Assert.IsNotNull(result);
            Assert.That(result.TranasactionId, Is.EqualTo(refund.TranasactionId));
        }

        [Test]
        public async Task GetRefundById_FailTest()
        {
            var result = await _refundRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetRefundByOrderId_PassTest()
        {
            var refund = new Refund
            {
                TranasactionId = 1,
                Amount = 50.00f,
                Status = RefundStatus.Intiated,
                OrderId = 1
            };
            refund = await _refundRepository.Add(refund);

            var result = await _refundRepository.GetRefundByOrderId(refund.OrderId);

            Assert.IsNotNull(result);
            Assert.That(result.OrderId, Is.EqualTo(refund.OrderId));
        }

        [Test]
        public async Task GetRefundByOrderId_FailTest()
        {
            var result = await _refundRepository.GetRefundByOrderId(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllRefunds_PassTest()
        {
            var refunds = new List<Refund>
            {
                new Refund
                {
                    TranasactionId = 1,
                    Amount = 50.00f,
                    Status = RefundStatus.Intiated,
                    OrderId = 1
                },
                new Refund
                {
                    TranasactionId = 2,
                    Amount = 30.00f,
                    Status = RefundStatus.Refunded,
                    OrderId = 2
                }
            };
            foreach (var refund in refunds)
            {
                await _refundRepository.Add(refund);
            }

            var result = await _refundRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(refunds, result);
        }

        [Test]
        public async Task GetAllRefunds_FailTest()
        {
            var result = await _refundRepository.GetAll();

            Assert.IsEmpty(result);
        }

    }
}
