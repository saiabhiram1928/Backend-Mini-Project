using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Exceptions;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class UserAuthServiceTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IHashService> _hashServiceMock;
        private Mock<IDTOService> _dtoServiceMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ITokenService> _tokenServiceMock;
        private UserAuthService _userAuthService;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _hashServiceMock = new Mock<IHashService>();
            _dtoServiceMock = new Mock<IDTOService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _userAuthService = new UserAuthService(
                _userRepositoryMock.Object,
                _hashServiceMock.Object,
                _dtoServiceMock.Object,
                _customerRepositoryMock.Object,
                _tokenServiceMock.Object);
        }
        [Test]
        public async Task LoginPassTest()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "TestPassword123!"
            };

            var user = new User
            {
                Uid = 1,
                Email = userLoginDTO.Email,
                Password = new byte[64], 
                Salt = new byte[128], 
                Verified = true,
                Role =Role.Customer
            };

            var customer = new Customer
            {
                Uid = user.Uid,
                User = user,
                MembershipType = MembershipType.Basic
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(userLoginDTO.Email)).ReturnsAsync(user);
            _hashServiceMock.Setup(service => service.AuthenticatePassword(userLoginDTO.Password, user.Salt, user.Password)).Returns(true);
            _customerRepositoryMock.Setup(repo => repo.GetById(user.Uid)).ReturnsAsync(customer);
            _tokenServiceMock.Setup(service => service.GenerateToken(user)).Returns("test-token");
            _dtoServiceMock.Setup(service => service.MapUserToUserReturnDTO(customer, "test-token")).Returns(new UserReturnDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Verified = user.Verified,
                MembershipType = customer.MembershipType,
                Role = user.Role,
                Token = "test-token"
            });

            // Act
            var result = await _userAuthService.Login(userLoginDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Verified, Is.EqualTo(user.Verified));
            Assert.That(result.MembershipType, Is.EqualTo(customer.MembershipType));
            Assert.That(result.Role, Is.EqualTo(user.Role));
            Assert.That(result.Token, Is.EqualTo("test-token"));
        }

        [Test]
        public void LoginFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO
            {
                Email = "nonexistent@example.com",
                Password = "TestPassword123!"
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(userLoginDTO.Email)).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userAuthService.Login(userLoginDTO));
        }
        [Test]
        public void LoginFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "WrongPassword123!"
            };

            var user = new User
            {
                Uid = 1,
                Email = userLoginDTO.Email,
                Password = new byte[64],
                Salt = new byte[128],
                Verified = true,
                Role = Role.Customer
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(userLoginDTO.Email)).ReturnsAsync(user);
            _hashServiceMock.Setup(service => service.AuthenticatePassword(userLoginDTO.Password, user.Salt, user.Password)).Returns(false);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(() => _userAuthService.Login(userLoginDTO));
        }

        [Test]
        public async Task RegisterPassTest()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = "newuser@example.com",
                Password = "TestPassword123!",
                FirstName = "John",
                LastName = "Doe"
            };

            var user = new User
            {
                Uid = 1,
                Email = userRegisterDTO.Email,
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Password = new byte[64], 
                Salt = new byte[128],
                Verified = true,
                Role = Role.Customer
            };

            var customer = new Customer
            {
                Uid = user.Uid,
                User = user,
                MembershipType = MembershipType.Basic
            };

            _userRepositoryMock.Setup(repo => repo.CheckUserExist(userRegisterDTO.Email)).ReturnsAsync(false);
            _hashServiceMock.Setup(service => service.HashPasswd(userRegisterDTO.Password)).Returns((new byte[64], new byte[128]));
            _dtoServiceMock.Setup(service => service.MapUserRegisterDTOToUser(userRegisterDTO, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync(user);
            _dtoServiceMock.Setup(service => service.MapUserRegisterDTOToCustomer(userRegisterDTO, user)).Returns(customer);
            _customerRepositoryMock.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(customer);
            _tokenServiceMock.Setup(service => service.GenerateToken(user)).Returns("test-token");
            _dtoServiceMock.Setup(service => service.MapUserToUserReturnDTO(customer, "test-token")).Returns(new UserReturnDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Verified = user.Verified,
                MembershipType = customer.MembershipType,
                Role = user.Role,
                Token = "test-token"
            });

            // Act
            var result = await _userAuthService.Register(userRegisterDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Verified, Is.EqualTo(user.Verified));
            Assert.That(result.MembershipType, Is.EqualTo(customer.MembershipType));
            Assert.That(result.Role, Is.EqualTo(user.Role));
            Assert.That(result.Token, Is.EqualTo("test-token"));
        }
        [Test]
        public void RegisterFailTest_ThrowsDuplicateItemException()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = "existinguser@example.com",
                Password = "TestPassword123!",
                FirstName = "Jane",
                LastName = "Doe"
            };

            _userRepositoryMock.Setup(repo => repo.CheckUserExist(userRegisterDTO.Email)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<DuplicateItemException>(() => _userAuthService.Register(userRegisterDTO));
        }

        [Test]
        public void Register_DbExceptionDuringUserCreation_ThrowsDbException()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = "newuser@example.com",
                Password = "TestPassword123!",
                FirstName = "John",
                LastName = "Doe"
            };

            _userRepositoryMock.Setup(repo => repo.CheckUserExist(userRegisterDTO.Email)).ReturnsAsync(false);
            _hashServiceMock.Setup(service => service.HashPasswd(userRegisterDTO.Password)).Returns((new byte[64], new byte[128]));
            _dtoServiceMock.Setup(service => service.MapUserRegisterDTOToUser(userRegisterDTO, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(new User());
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<DbException>(() => _userAuthService.Register(userRegisterDTO));
        }
        [Test]
        public void Register_DbExceptionDuringCustomerCreation_ThrowsDbException()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = "newuser@example.com",
                Password = "TestPassword123!",
                FirstName = "John",
                LastName = "Doe"
            };

            var user = new User
            {
                Uid = 1,
                Email = userRegisterDTO.Email,
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Password = new byte[64],
                Salt = new byte[128],
                Verified = true,
                Role = Role.Customer
            };

            _userRepositoryMock.Setup(repo => repo.CheckUserExist(userRegisterDTO.Email)).ReturnsAsync(false);
            _hashServiceMock.Setup(service => service.HashPasswd(userRegisterDTO.Password)).Returns((new byte[64], new byte[128]));
            _dtoServiceMock.Setup(service => service.MapUserRegisterDTOToUser(userRegisterDTO, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync(user);
            _dtoServiceMock.Setup(service => service.MapUserRegisterDTOToCustomer(userRegisterDTO, user)).Returns(new Customer());
            _customerRepositoryMock.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync((Customer)null);

            // Act & Assert
            Assert.ThrowsAsync<DbException>(() => _userAuthService.Register(userRegisterDTO));
        }
        

    }
}
