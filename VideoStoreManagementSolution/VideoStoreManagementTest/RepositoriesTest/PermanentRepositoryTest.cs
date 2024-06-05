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
    internal class PermanentRepositoryTest
    {
        private VideoStoreContext _context;
        private IPermanentRepository _permanentRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _permanentRepository = new PermanentRepository(_context);

           
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddPermanentPassTest()
        {
            var permanent = new Permanent
            {
                OrderId = 1,
                TotalQty = 5,
                TotalPrice = 50.00f
            };

            var addedPermanent = await _permanentRepository.Add(permanent);

            Assert.IsNotNull(addedPermanent);
            Assert.That(addedPermanent.TotalQty, Is.EqualTo(permanent.TotalQty));
        }

        [Test]
        public async Task AddPermanentFailTest()
        {
            var permanent = new Permanent
            {
                OrderId = 1,
                TotalQty = 5,
                TotalPrice = 50.00f
            };
            await _permanentRepository.Add(permanent);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _permanentRepository.Add(permanent);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdatePermanentPassTest()
        {
            var permanent = new Permanent
            {
                OrderId = 1,
                TotalQty = 5,
                TotalPrice = 50.00f
            };
            permanent = await _permanentRepository.Add(permanent);

            permanent.TotalPrice = 60.00f;
            var updatedPermanent = await _permanentRepository.Update(permanent);

            Assert.IsNotNull(updatedPermanent);
            Assert.That(updatedPermanent.TotalPrice, Is.EqualTo(60.00f));
        }

        [Test]
        public async Task UpdatePermanentFailTest()
        {
            var permanent = new Permanent
            {
                OrderId = 1,
                TotalQty = 5,
                TotalPrice = 50.00f
            };
            permanent = await _permanentRepository.Add(permanent);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                permanent.Id = 100;
                await _permanentRepository.Update(permanent);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeletePermanentPassTest()
        {
            var permanent = new Permanent
            {
                OrderId = 1,
                TotalQty = 5,
                TotalPrice = 50.00f
            };
            permanent = await _permanentRepository.Add(permanent);

            var result = await _permanentRepository.Delete(permanent.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeletePermanentFailTest()
        {
            var result = await _permanentRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetPermanentByIdPassTest()
        {
            var permanent = new Permanent
            {
                OrderId = 1,
                TotalQty = 5,
                TotalPrice = 50.00f
            };
            permanent = await _permanentRepository.Add(permanent);

            var result = await _permanentRepository.GetById(permanent.Id);

            Assert.IsNotNull(result);
            Assert.That(result.TotalQty, Is.EqualTo(permanent.TotalQty));
        }

        [Test]
        public async Task GetPermanentByIdFailTest()
        {
            var result = await _permanentRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllPermanentsPassTest()
        {
            var permanents = new List<Permanent>
            {
                new Permanent
                {
                    OrderId = 1,
                    TotalQty = 5,
                    TotalPrice = 50.00f
                },
                new Permanent
                {
                    OrderId = 2,
                    TotalQty = 3,
                    TotalPrice = 30.00f
                }
            };
            foreach (var permanent in permanents)
            {
                await _permanentRepository.Add(permanent);
            }

            var result = await _permanentRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(permanents, result);
        }

        [Test]
        public async Task GetAllPermanentsFailTest()
        {
            var result = await _permanentRepository.GetAll();

            Assert.IsEmpty(result);
        }
    }
}
