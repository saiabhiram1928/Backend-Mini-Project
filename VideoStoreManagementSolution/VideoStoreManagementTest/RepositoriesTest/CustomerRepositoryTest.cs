using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class CustomerRepositoryTest
    {
        VideoStoreContext _context;
        ICustomerRepository _customerRepository;
        IUserRepository _userRepository;
        User user;
        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder <VideoStoreContext> _optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>().UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(_optionsBuilder.Options);
            _customerRepository = new CustomerRepository(_context);
            _userRepository = new UserRepository(_context);


            using (var hmac = new HMACSHA512())
            {
               user =  new User() {  FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
               user =await _userRepository.Add(user);
            }

        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]
        public async Task AddCustomerPassTest()
        {
            //Arrange 

            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };

            //Action
            var addedCustomer = await _customerRepository.Add(customer);
             
            //Assert
            Assert.IsNotNull(addedCustomer);
            Assert.That(addedCustomer.Uid, Is.EqualTo(user.Uid));
            Assert.That(customer.Uid, Is.EqualTo(user.Uid));
        }
        [Test]
        public async Task AddUserFailTest()
        {
            //Arrange 

            var customer1 = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer1 = await _customerRepository.Add(customer1);

            //Action
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                var duplicateCustomer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
                await _customerRepository.Add(duplicateCustomer);
            });

            //Assert
            Assert.IsNotNull(customer1);
            Assert.IsNotNull(ex);
            Assert.That(ex.Message, Is.EqualTo("The instance of entity type 'Customer' cannot be tracked because another instance with the same key value for {'Uid'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values."));

        }
        [Test]
        public async Task UpdateCutomerPassTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);

            //Action

            customer.MembershipType = MembershipType.Premium;
            var updatedCustomer = await _customerRepository.Update(customer);

            //Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.IsNotNull(customer);
            Assert.That(updatedCustomer.MembershipType, Is.EqualTo(MembershipType.Premium));
        }
        [Test]
        public async Task UpdateCutomerFailTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);

            //Action
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                customer.Uid = 100;
                var updatedCustomer = await _customerRepository.Update(customer);
            });
           

            //Assert
          
            Assert.IsNotNull(customer);
            Assert.IsNotNull(ex);
            Assert.That(ex.Message, Is.EqualTo("The property 'Customer.Uid' is part of a key and so cannot be modified or marked as modified. To change the principal of an existing entity with an identifying foreign key, first delete the dependent and invoke 'SaveChanges', and then associate the dependent with the new principal."));
        }
        [Test]

        public async Task DeleteCustomerPassTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);

            //Action
            var res = await _customerRepository.Delete(user.Uid);

            //Assert
            Assert.IsNotNull(customer);
            Assert.IsTrue(res);
        }
        [Test]
        public async Task DeleteCustomerFailTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);
             await _customerRepository.Delete(user.Uid);
            //Action
           var res = await _customerRepository.Delete(user.Uid);
            //Assert
            Assert.IsNotNull(customer);
            Assert.IsFalse(res);
        }
        [Test]
        public async Task GetCustomerByIdPassTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);

            //Action
            var res = await _customerRepository.GetById(user.Uid);

            //Assert
            Assert.IsNotNull(customer);
            Assert.IsNotNull(res);
            Assert.That(res, Is.EqualTo(customer));
        }
        [Test]
        public async Task GetCustomerByIdFailTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);

            //Action
            var res = await _customerRepository.GetById(100);

            //Assert
            Assert.IsNotNull(customer);
            Assert.IsNull(res);
           
        }
        [Test]
        public async Task GetAllPassTest()
        {
            //Arrange

            List<Customer> customers = new List<Customer> {
             new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic },
            new Customer() { Uid = user.Uid + 1, MembershipType = MembershipType.Premium }
            };
          customers[0] =   await _customerRepository.Add(customers[0]);
          customers[1] =  await _customerRepository.Add(customers[1]);

            //Action
            var res = await _customerRepository.GetAll();

            //Assert
            Assert.IsNotNull(res);
            Assert.IsNotNull(customers[0]);
            Assert.IsNotNull(customers[1]);
            CollectionAssert.AreEquivalent(customers, res);
        }
        [Test]
        public async Task GetAllFailTest()
        {
            //Arrange
            List<Customer> customers = new List<Customer>();
            //Action
            var res = await _customerRepository.GetAll();

            //Assert
            Assert.IsEmpty(res);
            CollectionAssert.AreEquivalent(customers, res);
        }

        [Test]
        public async Task GetMembershipTypePassTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);
            //Action
            var res = await _customerRepository.GetMembershipType(user.Uid);

            //Assert
           Assert.IsNotNull(customer);
           Assert.IsNotNull(res);
           Assert.That(res, Is.EqualTo(MembershipType.Basic));
        }
        [Test]
        public async Task GetMembershipTypeFailTest()
        {
            //Arrange
            var customer = new Customer() { Uid = user.Uid, MembershipType = MembershipType.Basic };
            customer = await _customerRepository.Add(customer);
            //Action
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                 await _customerRepository.GetMembershipType(100);
            });

            //Assert
           Assert.IsNotNull(customer);
           Assert.IsNotNull(ex);
         
        }

    }
}
