using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Services;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class HashServiceTest
    {
        private HashServices _hashServices;

        [SetUp]
        public void Setup()
        {
            _hashServices = new HashServices();
        }
        [Test]
        public void HashPasswdPassTest()
        {
            // Arrange
            var password = "TestPassword123!";

            // Act
            var (hashedPassword, salt) = _hashServices.HashPasswd(password);

            // Assert
            Assert.IsNotNull(hashedPassword);
            Assert.IsNotNull(salt);
            Assert.IsTrue(hashedPassword.Length > 0);
            Assert.IsTrue(salt.Length > 0);
        }

        [Test]
        public void AuthenticatePasswordPassTest()
        {
            // Arrange
            var password = "TestPassword123!";
            var (hashedPassword, salt) = _hashServices.HashPasswd(password);

            // Act
            var result = _hashServices.AuthenticatePassword(password, salt, hashedPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AuthenticatePasswordFailTest_ShouldReturnFalse()
        {
            // Arrange
            var password = "TestPassword123!";
            var (hashedPassword, salt) = _hashServices.HashPasswd(password);
            var wrongPassword = "WrongPassword123!";

            // Act
            var result = _hashServices.AuthenticatePassword(wrongPassword, salt, hashedPassword);

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void AuthenticatePasswordFailTest_WithDifferentSalt_ShouldReturnFalse()
        {
            // Arrange
            var password = "TestPassword123!";
            var (hashedPassword, salt) = _hashServices.HashPasswd(password);
            var wrongSalt = new HMACSHA512().Key;

            // Act
            var result = _hashServices.AuthenticatePassword(password, wrongSalt, hashedPassword);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
