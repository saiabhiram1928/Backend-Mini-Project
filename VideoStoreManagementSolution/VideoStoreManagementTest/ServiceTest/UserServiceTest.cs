using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services;
using VideoStoreManagementApi.Exceptions;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class UserServiceTest
    {
        private VideoStoreContext _context;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IDTOService> _dtoServiceMock;
        private Mock<IAddressRepository> _addressRepositoryMock;
        private Mock<IHashService> _hashServiceMock;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
              .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _tokenServiceMock = new Mock<ITokenService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _dtoServiceMock = new Mock<IDTOService>();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _hashServiceMock = new Mock<IHashService>();
            _userService = new UserService(
                _tokenServiceMock.Object,
                _customerRepositoryMock.Object,
                _userRepositoryMock.Object,
                _dtoServiceMock.Object,
                _addressRepositoryMock.Object,
                _hashServiceMock.Object);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAddressPassTestReturnsAddressDTO()
        {
            // Arrange
            var addressRegisterDTO = new AddressRegisterDTO
            {
                Area = "Test Area",
                City = "Test City",
                State = "Test State",
                Zipcode = 12345,
                PrimaryAdress = true
            };

            var addressDTO = new AddressDTO
            {
                Id = 1,
                Area = addressRegisterDTO.Area,
                City = addressRegisterDTO.City,
                State = addressRegisterDTO.State,
                Zipcode = addressRegisterDTO.Zipcode,
                PrimaryAdress = addressRegisterDTO.PrimaryAdress
            };

            var address = new Address
            {
                Id = addressDTO.Id,
                Area = addressDTO.Area,
                City = addressDTO.City,
                State = addressDTO.State,
                Zipcode = addressDTO.Zipcode,
                PrimaryAdress = addressDTO.PrimaryAdress,
                CustomerId = 1
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.CheckUserExist(1)).ReturnsAsync(true);
            _addressRepositoryMock.Setup(repo => repo.MakePrimaryAddressFalse(1)).Returns(Task.CompletedTask);
            _addressRepositoryMock.Setup(repo => repo.Add(It.IsAny<Address>())).ReturnsAsync(address);
            _dtoServiceMock.Setup(service => service.MapAddressRegisterDTOTOAddressDTO(addressRegisterDTO)).Returns(addressDTO);
            _dtoServiceMock.Setup(service => service.MapAddressDTOToAddress(addressDTO, It.IsAny<Address>())).Returns(address);
            _dtoServiceMock.Setup(service => service.MapAddressToAddressDTO(address)).Returns(addressDTO);

            // Act
            var result = await _userService.AddAddress(addressRegisterDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Area, Is.EqualTo(addressDTO.Area));
            Assert.That(result.City, Is.EqualTo(addressDTO.City));
            Assert.That(result.State, Is.EqualTo(addressDTO.State));
            Assert.That(result.Zipcode, Is.EqualTo(addressDTO.Zipcode));
            Assert.That(result.PrimaryAdress, Is.EqualTo(addressDTO.PrimaryAdress));
        }
        [Test]
        public void AddAddressFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            var addressRegisterDTO = new AddressRegisterDTO
            {
                Area = "Test Area",
                City = "Test City",
                State = "Test State",
                Zipcode = 12345,
                PrimaryAdress = true
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(() => _userService.AddAddress(addressRegisterDTO));
        }
        [Test]
        public void AddAddressFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            var addressRegisterDTO = new AddressRegisterDTO
            {
                Area = "Test Area",
                City = "Test City",
                State = "Test State",
                Zipcode = 12345,
                PrimaryAdress = true
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.CheckUserExist(1)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userService.AddAddress(addressRegisterDTO));
        }

        [Test]
        public async Task ChangePasswordPassTest_ReturnsSuccessMessage()
        {
            // Arrange
            string newPasswd = "NewPassword123!";
            string oldPasswd = "OldPassword123!";
            var user = new User
            {
                Uid = 1,
                Password = new byte[64], // Example byte array
                Salt = new byte[128] // Example byte array
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(user);
            _hashServiceMock.Setup(service => service.AuthenticatePassword(oldPasswd, user.Salt, user.Password)).Returns(true);
            _hashServiceMock.Setup(service => service.HashPasswd(newPasswd)).Returns((new byte[64], new byte[128]));
            _userRepositoryMock.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.ChangePassword(newPasswd, oldPasswd);

            // Assert
            Assert.That(result, Is.EqualTo("Sucessfully Updated Password"));
        }

        [Test]
        public void ChangePasswordFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            string newPasswd = "NewPassword123!";
            string oldPasswd = "OldPassword123!";

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(() => _userService.ChangePassword(newPasswd, oldPasswd));
        }
        [Test]
        public void ChangePasswordFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            string newPasswd = "NewPassword123!";
            string oldPasswd = "OldPassword123!";

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userService.ChangePassword(newPasswd, oldPasswd));
        }

        [Test]
        public void ChangePasswordFailTest_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            string newPasswd = "NewPassword123!";
            string oldPasswd = "OldPassword123!";
            var user = new User
            {
                Uid = 1,
                Password = new byte[64], // Example byte array
                Salt = new byte[128] // Example byte array
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(user);
            _hashServiceMock.Setup(service => service.AuthenticatePassword(oldPasswd, user.Salt, user.Password)).Returns(false);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.ChangePassword(newPasswd, oldPasswd));
        }
        [Test]
        public async Task DeleteAddressPassTest()
        {
            // Arrange
            var address = new Address
            {
                Id = 1,
                CustomerId = 1
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(address);
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _addressRepositoryMock.Setup(repo => repo.Delete(1)).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteAddress(1);

            // Assert
            Assert.That(result, Is.EqualTo("Sucess"));
        }

        [Test]
        public void DeleteAddressFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((Address)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userService.DeleteAddress(1));
        }
        [Test]
        public void DeleteAddressFailTest_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var address = new Address
            {
                Id = 1,
                CustomerId = 2
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(address);
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.DeleteAddress(1));
        }
        [Test]
        public void DeleteAddressFailTest_ThrowsDbException()
        {
            // Arrange
            var address = new Address
            {
                Id = 1,
                CustomerId = 1
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(address);
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _addressRepositoryMock.Setup(repo => repo.Delete(1)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<DbException>(() => _userService.DeleteAddress(1));
        }
        [Test]
        public async Task EditAddressPassTest()
        {
            // Arrange
            var addressDTO = new AddressDTO
            {
                Id = 1,
                Area = "New Area",
                City = "New City",
                State = "New State",
                Zipcode = 54321,
                PrimaryAdress = true
            };

            var address = new Address
            {
                Id = 1,
                Area = "Old Area",
                City = "Old City",
                State = "Old State",
                Zipcode = 12345,
                PrimaryAdress = false,
                CustomerId = 1
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(address);
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _addressRepositoryMock.Setup(repo => repo.MakePrimaryAddressFalse(1)).Returns(Task.CompletedTask);
            _addressRepositoryMock.Setup(repo => repo.Update(It.IsAny<Address>())).ReturnsAsync(address);
            _dtoServiceMock.Setup(service => service.MapAddressDTOToAddress(addressDTO, address)).Returns(address);

            // Act
            var result = await _userService.EditAddress(addressDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(addressDTO.Id));
            Assert.That(result.Area, Is.EqualTo(addressDTO.Area));
            Assert.That(result.City, Is.EqualTo(addressDTO.City));
            Assert.That(result.State, Is.EqualTo(addressDTO.State));
            Assert.That(result.Zipcode, Is.EqualTo(addressDTO.Zipcode));
            Assert.That(result.PrimaryAdress, Is.EqualTo(addressDTO.PrimaryAdress));
        }
        [Test]
        public void EditAddressFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            var addressDTO = new AddressDTO
            {
                Id = 1
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((Address)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userService.EditAddress(addressDTO));
        }

        [Test]
        public void EditAddressFailTest_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var addressDTO = new AddressDTO
            {
                Id = 1
            };

            var address = new Address
            {
                Id = 1,
                CustomerId = 2
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(address);
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.EditAddress(addressDTO));
        }
        [Test]
        public void EditAddresseFailTest_ThrowsDbException()
        {
            // Arrange
            var addressDTO = new AddressDTO
            {
                Id = 1
            };

            var address = new Address
            {
                Id = 1,
                CustomerId = 1
            };

            _addressRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(address);
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _addressRepositoryMock.Setup(repo => repo.Update(It.IsAny<Address>())).ReturnsAsync((Address)null);

            // Act & Assert
            Assert.ThrowsAsync<DbException>(() => _userService.EditAddress(addressDTO));
        }
        [Test]
        public async Task EditProfilePassTest()
        {

            // Arrange
            var userProfileEditDTO = new UserProfileEditDTO
            {
                FirstName = "NewFirstName",
                LastName = "NewLastName"
            };

            var user = new User
            {
                Uid = 1,
                FirstName = "OldFirstName",
                LastName = "OldLastName"
            };

            var customer = new Customer
            {
                Uid = 1
            };

            var userReturnDTO = new UserReturnDTO
            {
                FirstName = userProfileEditDTO.FirstName,
                LastName = userProfileEditDTO.LastName
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(user);
            _customerRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(customer);
            _dtoServiceMock.Setup(service => service.MapUserToUserReturnDTO(customer, "")).Returns(userReturnDTO);

            // Act
            var result = await _userService.EditProfile(userProfileEditDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.FirstName, Is.EqualTo(userProfileEditDTO.FirstName));
            Assert.That(result.LastName, Is.EqualTo(userProfileEditDTO.LastName));
        }

        [Test]
        public void EditProfileFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            var userProfileEditDTO = new UserProfileEditDTO
            {
                FirstName = "NewFirstName",
                LastName = "NewLastName"
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(() => _userService.EditProfile(userProfileEditDTO));
        }

        [Test]
        public void EditProfileFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            var userProfileEditDTO = new UserProfileEditDTO
            {
                FirstName = "NewFirstName",
                LastName = "NewLastName"
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userService.EditProfile(userProfileEditDTO));
        }
        [Test]
        public async Task ViewProfilePassTest()
        {
            // Arrange
            var customer = new Customer
            {
                Uid = 1
            };

            var userReturnDTO = new UserReturnDTO
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _customerRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(customer);
            _dtoServiceMock.Setup(service => service.MapUserToUserReturnDTO(customer, "")).Returns(userReturnDTO);

            // Act
            var result = await _userService.ViewProfile();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.FirstName, Is.EqualTo(userReturnDTO.FirstName));
            Assert.That(result.LastName, Is.EqualTo(userReturnDTO.LastName));
        }

        [Test]
        public void ViewProfileFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(() => _userService.ViewProfile());
        }
        [Test]
        public void ViewProfileFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _customerRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((Customer)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _userService.ViewProfile());
        }


        [Test]
        public async Task ViewAddresFailTest_ReturnsAddressDTOList()
        {
            // Arrange
            var addresses = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    Area = "Area1",
                    City = "City1",
                    State = "State1",
                    Zipcode = 12345,
                    PrimaryAdress = true
                },
                new Address
                {
                    Id = 2,
                    Area = "Area2",
                    City = "City2",
                    State = "State2",
                    Zipcode = 54321,
                    PrimaryAdress = false
                }
            };

            var addressDTOList = new List<AddressDTO>
            {
                new AddressDTO
                {
                    Id = 1,
                    Area = "Area1",
                    City = "City1",
                    State = "State1",
                    Zipcode = 12345,
                    PrimaryAdress = true
                },
                new AddressDTO
                {
                    Id = 2,
                    Area = "Area2",
                    City = "City2",
                    State = "State2",
                    Zipcode = 54321,
                    PrimaryAdress = false
                }
            };

            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.CheckUserExist(1)).ReturnsAsync(true);
            _addressRepositoryMock.Setup(repo => repo.GetAllAdressOfUser(1)).ReturnsAsync(addresses);
            _dtoServiceMock.Setup(service => service.MapAddressToAddressDTO(It.IsAny<Address>()))
                .Returns((Address a) => new AddressDTO
                {
                    Id = a.Id,
                    Area = a.Area,
                    City = a.City,
                    State = a.State,
                    Zipcode = a.Zipcode,
                    PrimaryAdress = a.PrimaryAdress
                });

            // Act
            var result = await _userService.ViewAddress();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(addressDTOList.Count));
            for (int i = 0; i < addressDTOList.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(addressDTOList[i].Id));
                Assert.That(result[i].Area, Is.EqualTo(addressDTOList[i].Area));
                Assert.That(result[i].City, Is.EqualTo(addressDTOList[i].City));
                Assert.That(result[i].State, Is.EqualTo(addressDTOList[i].State));
                Assert.That(result[i].Zipcode, Is.EqualTo(addressDTOList[i].Zipcode));
                Assert.That(result[i].PrimaryAdress, Is.EqualTo(addressDTOList[i].PrimaryAdress));
            }
        }

        [Test]
        public void ViewAddressFailTest_ThrowsUnauthorizedUserException()
        {
            // Arrange
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(() => _userService.ViewAddress());
        }
        [Test]
        public void ViewAddressFailTest_ThrowsNoItemsInDbException()
        {
            // Arrange
            _tokenServiceMock.Setup(service => service.GetUidFromToken()).Returns(1);
            _userRepositoryMock.Setup(repo => repo.CheckUserExist(1)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInDbException>(() => _userService.ViewAddress());
        }
    }
}
