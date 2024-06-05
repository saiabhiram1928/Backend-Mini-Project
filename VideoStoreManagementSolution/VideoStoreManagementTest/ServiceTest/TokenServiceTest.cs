
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services;

namespace VideoStoreManagementTest.ServiceTest
{
    public class TokenServiceTest
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        [SetUp]
        public void Setup()
        {
            string secretKey = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";
            Mock<IConfigurationSection> configurationJWTSectionMock = new Mock<IConfigurationSection>();
            configurationJWTSectionMock.Setup(x => x.Value).Returns(secretKey);
            Mock<IConfigurationSection> configurationTokenKeySectionMock = new Mock<IConfigurationSection>();
            configurationTokenKeySectionMock.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSectionMock.Object);
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(x => x.GetSection("TokenKey")).Returns(configurationTokenKeySectionMock.Object);

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        }

        [Test]
        public void CreateTokenPassTest()
        {
            ITokenService service = new TokenService(_configurationMock.Object, _httpContextAccessorMock.Object);

            var user = new User() { Uid = 123, Role = VideoStoreManagementApi.Models.Enums.Role.Admin };
            var token = service.GenerateToken(user);

            Assert.IsNotNull(token);
        }

        [Test]
        public void CheckTokenClaimTest()
        {

            // Arrange
            ITokenService service = new TokenService(_configurationMock.Object, _httpContextAccessorMock.Object);

            // Act
            var token = service.GenerateToken(new User { Uid = 123, Role = VideoStoreManagementApi.Models.Enums.Role.Admin });
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.That(jwtToken.Claims.FirstOrDefault(c => c.Type == "eid")?.Value, Is.EqualTo("123"));
            Assert.That(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, Is.EqualTo("Admin"));
        }

        [Test]
        public void TokenHasCorrectExpirationTest()
        {
            // Arrange
            ITokenService service = new TokenService(_configurationMock.Object, _httpContextAccessorMock.Object);

            // Act
            var token = service.GenerateToken(new User { Uid = 123, Role = VideoStoreManagementApi.Models.Enums.Role.Admin });
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.IsTrue(jwtToken.ValidTo <= DateTime.UtcNow.AddDays(2));
        }
        [Test]
        public void GenerateTokenInvalidConfigurationTest()
        {
            // Arrange
            var invalidConfigurationMock = new Mock<IConfiguration>();
            var emptyJWTSectionMock = new Mock<IConfigurationSection>();
            emptyJWTSectionMock.Setup(x => x.Value).Returns((string)null);
            invalidConfigurationMock.Setup(x => x.GetSection("TokenKey:JWT")).Returns(emptyJWTSectionMock.Object);

            ITokenService service;

            // Act
            var ex = Assert.Throws<NullReferenceException>(() => service = new TokenService(invalidConfigurationMock.Object, _httpContextAccessorMock.Object));

            //Assert
            Assert.That(ex.Message, Is.EqualTo("JWt Secret Key is Null"));
        }

        [Test]
        public void CreateTokenForVaraiousRoleTest()
        {
            // Arrange
            ITokenService service = new TokenService(_configurationMock.Object, _httpContextAccessorMock.Object);
            var handler = new JwtSecurityTokenHandler();

            // Act Customer
            var token1 = service.GenerateToken(new User { Uid = 123, Role = VideoStoreManagementApi.Models.Enums.Role.Customer });
            var jwtToken1 = handler.ReadJwtToken(token1);

            // Act Employee

            var token2 = service.GenerateToken(new User { Uid = 123, Role = VideoStoreManagementApi.Models.Enums.Role.Employee });
            var jwtToken2 = handler.ReadJwtToken(token2);

            // Assert Customer
            Assert.That(jwtToken1.Claims.FirstOrDefault(c => c.Type == "eid")?.Value, Is.EqualTo("123"));
            Assert.That(jwtToken1.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, Is.EqualTo("Customer"));

            //Assert Employee
            Assert.That(jwtToken2.Claims.FirstOrDefault(c => c.Type == "eid")?.Value, Is.EqualTo("123"));
            Assert.That(jwtToken2.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, Is.EqualTo("Employee"));
        }

    }
}