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
    internal class PaymentRepositoryTest
    {
        private VideoStoreContext _context;
        private IPaymentRepository _paymentRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test")
                .Options;

            _context = new VideoStoreContext(options);
            _paymentRepository = new PaymentRespository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddPayment_ShouldAddPayment()
        {
            var payment = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };

            var addedPayment = await _paymentRepository.Add(payment);

            Assert.IsNotNull(addedPayment);
            Assert.That(addedPayment.TransactionId, Is.EqualTo(payment.TransactionId));
        }

        [Test]
        public async Task GetAll_ShouldReturnAllPayments()
        {
            var payment1 = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };
            var payment2 = new Payment
            {
                TransactionId = 2,
                Amount = 200.0f,
                OrderId = 2,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };

            await _paymentRepository.Add(payment1);
            await _paymentRepository.Add(payment2);

            var payments = await _paymentRepository.GetAll();

            Assert.That(payments.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetById_ShouldReturnPayment()
        {
            var payment = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };

            var addedPayment = await _paymentRepository.Add(payment);

            var fetchedPayment = await _paymentRepository.GetById(addedPayment.TransactionId);

            Assert.IsNotNull(fetchedPayment);
            Assert.That(fetchedPayment.TransactionId, Is.EqualTo(addedPayment.TransactionId));
        }

        [Test]
        public async Task UpdatePayment_ShouldUpdatePayment()
        {
            var payment = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };

            var addedPayment = await _paymentRepository.Add(payment);

            addedPayment.Amount = 150.0f;
            var updatedPayment = await _paymentRepository.Update(addedPayment);

            Assert.IsNotNull(updatedPayment);
            Assert.That(updatedPayment.Amount, Is.EqualTo(150.0f));
        }

        [Test]
        public async Task DeletePayment_ShouldDeletePayment()
        {
            var payment = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };

            var addedPayment = await _paymentRepository.Add(payment);

            var result = await _paymentRepository.Delete(addedPayment.TransactionId);

            Assert.IsTrue(result);

            var fetchedPayment = await _paymentRepository.GetById(addedPayment.TransactionId);
            Assert.IsNull(fetchedPayment);
        }

        [Test]
        public async Task GetPaymentByOrderId_ShouldReturnPayment()
        {
            var payment = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = true
            };

            await _paymentRepository.Add(payment);

            var fetchedPayment = await _paymentRepository.GetPaymentByOrderId(1);

            Assert.IsNotNull(fetchedPayment);
            Assert.That(fetchedPayment.TransactionId, Is.EqualTo(payment.TransactionId));
        }

        [Test]
        public async Task GetPaymentByOrderId_ShouldReturnNullIfPaymentNotSuccessful()
        {
            var payment = new Payment
            {
                TransactionId = 1,
                Amount = 100.0f,
                OrderId = 1,
                PaymentDate = DateTime.Now,
                PaymentSucess = false
            };

            await _paymentRepository.Add(payment);

            var fetchedPayment = await _paymentRepository.GetPaymentByOrderId(1);

            Assert.IsNull(fetchedPayment);
        }
    }
}
