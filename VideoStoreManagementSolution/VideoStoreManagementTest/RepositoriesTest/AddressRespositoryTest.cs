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
    internal class AddressRespositoryTest
    {
        private VideoStoreContext _context;
        private IAddressRepository _addressRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _addressRepository = new AddressRepsitory(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAddressPassTest()
        {
            var address = new Address
            {
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = 1
            };

            var addedAddress = await _addressRepository.Add(address);

            Assert.IsNotNull(addedAddress);
            Assert.That(addedAddress.Area, Is.EqualTo(address.Area));
        }

        [Test]
        public async Task AddAddressFailTest()
        {
           
            
             var ex =   await _addressRepository.Add(null);
           
            Assert.IsNull(ex);
        }

        [Test]
        public async Task DeleteAddressPassTest()
        {
            var address = new Address
            {
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = 1
            };
            address = await _addressRepository.Add(address);

            var result = await _addressRepository.Delete(address.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteAddressFailTest()
        {
            var result = await _addressRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateAddressPassTest()
        {
            var address = new Address
            {
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = 1
            };
            address = await _addressRepository.Add(address);

            address.Area = "New Area";
            var updatedAddress = await _addressRepository.Update(address);

            Assert.IsNotNull(updatedAddress);
            Assert.That(updatedAddress.Area, Is.EqualTo("New Area"));
        }

        [Test]
        public async Task UpdateAddressFailTest()
        {
            var address = new Address
            {
                Id = 100,
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = 1
            };

            var ex = Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
            {
                await _addressRepository.Update(address);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task GetAllAddressesPassTest()
        {
            var addresses = new List<Address>
            {
                new Address
                {
                    Area = "Area1",
                    City = "City1",
                    State = "State1",
                    Zipcode = 12345,
                    CustomerId = 1
                },
                new Address
                {
                    Area = "Area2",
                    City = "City2",
                    State = "State2",
                    Zipcode = 54321,
                    CustomerId = 2
                }
            };

            await _addressRepository.Add(addresses[0]);
            await _addressRepository.Add(addresses[1]);

            var result = await _addressRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(addresses, result);
        }

        [Test]
        public async Task GetAllAddressesFailTest()
        {
            var result = await _addressRepository.GetAll();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAddressByIdPassTest()
        {
            var address = new Address
            {
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = 1
            };
            address = await _addressRepository.Add(address);

            var result = await _addressRepository.GetById(address.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Area, Is.EqualTo(address.Area));
        }

        [Test]
        public async Task GetAddressByIdFailTest()
        {
            var result = await _addressRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllAddressOfUserPassTest()
        {
            var customerId = 1;
            var addresses = new List<Address>
            {
                new Address
                {
                    Area = "Area1",
                    City = "City1",
                    State = "State1",
                    Zipcode = 12345,
                    CustomerId = customerId
                },
                new Address
                {
                    Area = "Area2",
                    City = "City2",
                    State = "State2",
                    Zipcode = 54321,
                    CustomerId = 2
                }
            };

            await _addressRepository.Add(addresses[0]);
            await _addressRepository.Add(addresses[1]);

            var result = await _addressRepository.GetAllAdressOfUser(customerId);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Area, Is.EqualTo(addresses[0].Area));
        }

        [Test]
        public async Task MakePrimaryAddressFalsePassTest()
        {
            var customerId = 1;
            var address = new Address
            {
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = customerId,
                PrimaryAdress = true
            };
            await _addressRepository.Add(address);

            await _addressRepository.MakePrimaryAddressFalse(customerId);
            var result = await _addressRepository.GetById(address.Id);

            Assert.IsFalse(result.PrimaryAdress);
        }

        [Test]
        public async Task CheckAddressIsOfUserPassTest()
        {
            var customerId = 1;
            var address = new Address
            {
                Area = "Area",
                City = "City",
                State = "State",
                Zipcode = 12345,
                CustomerId = customerId
            };
            await _addressRepository.Add(address);

            var result = await _addressRepository.CheckAddressIsOfUser(customerId, address.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckAddressIsOfUserFailTest()
        {
            var customerId = 1;
            var addressId = 100;

            var result = await _addressRepository.CheckAddressIsOfUser(customerId, addressId);

            Assert.IsFalse(result);
        }

    }
}
