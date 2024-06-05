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
    internal class RentalRepositortTest
    {
        private VideoStoreContext _context;
        private IRentalRepository _rentalRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _rentalRepository = new RentalRepository(_context);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddRentalPassTest()
        {
            var rental = new Rental
            {
                OrderId = 1,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(10),
                TotalQty = 5,
                TotalPrice = 50.00f,
                LateFee = 10.00f
            };

            var addedRental = await _rentalRepository.Add(rental);

            Assert.IsNotNull(addedRental);
            Assert.That(addedRental.TotalQty, Is.EqualTo(rental.TotalQty));
        }

        [Test]
        public async Task AddRentalFailTest()
        {
            var rental = new Rental
            {
                OrderId = 1,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(10),
                TotalQty = 5,
                TotalPrice = 50.00f,
                LateFee = 10.00f
            };
            await _rentalRepository.Add(rental);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _rentalRepository.Add(rental);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdateRentalPassTest()
        {
            var rental = new Rental
            {
                OrderId = 1,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(10),
                TotalQty = 5,
                TotalPrice = 50.00f,
                LateFee = 10.00f
            };
            rental = await _rentalRepository.Add(rental);

            rental.TotalPrice = 60.00f;
            var updatedRental = await _rentalRepository.Update(rental);

            Assert.IsNotNull(updatedRental);
            Assert.That(updatedRental.TotalPrice, Is.EqualTo(60.00f));
        }

        [Test]
        public async Task UpdateRentalFailTest()
        {
            var rental = new Rental
            {
                OrderId = 1,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(10),
                TotalQty = 5,
                TotalPrice = 50.00f,
                LateFee = 10.00f
            };
            rental = await _rentalRepository.Add(rental);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                rental.Id = 100;
                await _rentalRepository.Update(rental);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeleteRentalPassTest()
        {
            var rental = new Rental
            {
                OrderId = 1,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(10),
                TotalQty = 5,
                TotalPrice = 50.00f,
                LateFee = 10.00f
            };
            rental = await _rentalRepository.Add(rental);

            var result = await _rentalRepository.Delete(rental.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteRentalFailTest()
        {
            var result = await _rentalRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetRentalByIdPassTest()
        {
            var rental = new Rental
            {
                OrderId = 1,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(10),
                TotalQty = 5,
                TotalPrice = 50.00f,
                LateFee = 10.00f
            };
            rental = await _rentalRepository.Add(rental);

            var result = await _rentalRepository.GetById(rental.Id);

            Assert.IsNotNull(result);
            Assert.That(result.TotalQty, Is.EqualTo(rental.TotalQty));
        }

        [Test]
        public async Task GetRentalByIdFailTest()
        {
            var result = await _rentalRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllRentalsPassTest()
        {
            var rentals = new List<Rental>
            {
                new Rental
                {
                    OrderId = 1,
                    RentDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    ReturnDate = DateTime.Now.AddDays(10),
                    TotalQty = 5,
                    TotalPrice = 50.00f,
                    LateFee = 10.00f
                },
                new Rental
                {
                    OrderId = 2,
                    RentDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    ReturnDate = DateTime.Now.AddDays(10),
                    TotalQty = 3,
                    TotalPrice = 30.00f,
                    LateFee = 5.00f
                }
            };
           
            
              rentals[0] =   await _rentalRepository.Add(rentals[0]);
              rentals[1] =   await _rentalRepository.Add(rentals[1]);
            

            var result = await _rentalRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(rentals, result);
        }

        [Test]
        public async Task GetAllRentalsFailTest()
        {
            var result = await _rentalRepository.GetAll();
            
            Assert.IsEmpty(result);
        }
        [Test]
       
        public void DueDateShouldBeGreaterThanRentDate()
        {
            var rentDate = DateTime.Now;
            var dueDate = rentDate.AddDays(1);
            var returnDate = rentDate.AddDays(2);

            var rental = new Rental
            {
                RentDate = rentDate,
                DueDate = dueDate,
                ReturnDate = returnDate
            };

            Assert.AreEqual(dueDate.Date, rental.DueDate.Date);
        }

        [Test]
        public void DueDateShouldFailIfNotGreaterThanRentDate()
        {
            var rental = new Rental
            {
                RentDate = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() => rental.DueDate = DateTime.Now.AddDays(-1), "DueDate must be greater than RentDate");
        }

        [Test]
        public void ReturnDateShouldFailIfNotGreaterThanRentDate()
        {
            var rental = new Rental
            {
                RentDate = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() => rental.ReturnDate = DateTime.Now.AddDays(-1), "ReturnDate must be greater than RentDate");
        }

        [Test]

        public void ReturnDateShouldBeGreaterThanRentDate()
        {
            var rentDate = DateTime.Now;
            var dueDate = rentDate.AddDays(2);
            var returnDate = rentDate.AddDays(3);

            var rental = new Rental
            {
                RentDate = rentDate,
                DueDate = dueDate,
                ReturnDate = returnDate
            };

            Assert.That(rental.ReturnDate.Date, Is.EqualTo(returnDate.Date));
        }

    }
}
