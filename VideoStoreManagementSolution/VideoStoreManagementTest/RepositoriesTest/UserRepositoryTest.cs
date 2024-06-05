using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class UserRepositoryTest
    {
        private VideoStoreContext _context;
        private IUserRepository _userRepository;

        [SetUp]
        public  void SetUp()
        {
            DbContextOptionsBuilder<VideoStoreContext> _optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>().UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(_optionsBuilder.Options);
            _userRepository = new UserRepository(_context);

            
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]
        public async Task AddUserPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test1234")), Salt = hmac.Key, Verified = true };

            // Act
            var addedUser = await _userRepository.Add(user);

            // Assert
            Assert.IsNotNull(addedUser);
            Assert.That(addedUser.Email, Is.EqualTo(user.Email));
            Assert.That(user.Uid, Is.EqualTo(addedUser.Uid));
        }


        [Test]
        public async Task UpdateUserPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test", LastName = "user", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            user = await _userRepository.Add(user);

            // Act
            user.FirstName = "test2updated";
            var updatedUser = await _userRepository.Update(user);

            // Assert
            Assert.IsNotNull(updatedUser);
            Assert.That(updatedUser.FirstName, Is.EqualTo("test2updated"));
        }
        [Test]
        public async Task UpdateUserFailTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            user = await _userRepository.Add(user);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                user.Uid = 100; 
                await _userRepository.Update(user);
            });
            Assert.IsNotNull(ex);
        }
        [Test]
        public async Task DeleteUserPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            user = await _userRepository.Add(user);

            // Act
            var result = await _userRepository.Delete(user.Uid);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public async Task DeleteUserFailTest()
        {
            // Act
            var result = await _userRepository.Delete(999); 

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetUserByIdPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            user = await _userRepository.Add(user);

            // Act
            var retrievedUser = await _userRepository.GetById(user.Uid);

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.That(retrievedUser.Email, Is.EqualTo(user.Email));
        }
        [Test]
        public async Task GetUserByIdFailTest()
        {
            // Act
            var retrievedUser = await _userRepository.GetById(999); 
            // Assert
            Assert.IsNull(retrievedUser);
        }

        [Test]
        public async Task GetAllUsersPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var users = new List<User>() {
            new User { FirstName = "test1", LastName = "user1", Email = "testuser1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("password")), Salt = hmac.Key, Verified = true },
             new User { FirstName = "test2", LastName = "user2", Email = "testuser2@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("password")), Salt = hmac.Key, Verified = true }
             };
           users[0] = await _userRepository.Add(users[0]);
           users[1]=  await _userRepository.Add(users[1]);

            // Act
            var res = await _userRepository.GetAll();

            // Assert
            Assert.IsNotNull(users);
            CollectionAssert.AreEquivalent(users, res);
        }

        [Test]
        public async Task GetAllUsersFailTest()
        {
            // Act
            var users = await _userRepository.GetAll();

            // Assert
            Assert.IsEmpty(users);
        }
        [Test]
        public async Task GetUserByEmailPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            await _userRepository.Add(user);

            // Act
            var retrievedUser = await _userRepository.GetUserByEmail("test1@gmail.com");

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.That(retrievedUser.Email, Is.EqualTo(user.Email));
        }
        [Test]
        public async Task GetUserByEmailFailTest()
        {
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            await _userRepository.Add(user);
            // Act
            var retrievedUser = await _userRepository.GetUserByEmail("test2@gmail.com");

            // Assert
            Assert.IsNull(retrievedUser);
        }
        [Test]
        public async Task CheckUserExistByEmailPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            await _userRepository.Add(user);

            // Act
            var exists = await _userRepository.CheckUserExist("test1@gmail.com");

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task CheckUserExistByEmailFailTest()
        {
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            await _userRepository.Add(user);
            // Act
            var exists = await _userRepository.CheckUserExist("test@gmail.com");

            // Assert
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task CheckUserExistByIdPassTest()
        {
            // Arrange
            using var hmac = new HMACSHA512();
            var user = new User { FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt = hmac.Key, Verified = true };
            user = await _userRepository.Add(user);

            // Act
            var exists = await _userRepository.CheckUserExist(user.Uid);

            // Assert
            Assert.IsTrue(exists);
        }
        [Test]
        public async Task CheckUserExistByIdFailTest()
        {
            // Act
            var exists = await _userRepository.CheckUserExist(999); 

            // Assert
            Assert.IsFalse(exists);
        }


    }
}
