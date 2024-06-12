using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Services
{
    public class TokenService : ITokenService
    {
        private string _secretKey;
        private SymmetricSecurityKey _key;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region Constructor
        public TokenService(IConfiguration configuration , IHttpContextAccessor httpContextAccessor)
        {
            _secretKey = configuration?.GetSection("TokenKey")?.GetSection("JWT")?.Value.ToString();
            if (string.IsNullOrEmpty(_secretKey))
            {
                throw new NullReferenceException("JWt Secret Key is Null");
            }
            
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region GenerateToken
        /// <summary>
        /// Generates based on User credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token in string data type</returns>
        public string GenerateToken(User user)
        {
            var claim = new List<Claim>() {
                new Claim("eid", user.Uid.ToString()),
                new Claim("iat" ,  DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new Claim("exp" ,  DateTimeOffset.Now.AddDays(2).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role , user.Role.ToString())
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
             null,
             null, 
             claim, 
             expires: DateTime.Now.AddDays(2), 
             signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region GetUidFromToken
        /// <summary>
        /// Extract User Id from Token
        /// </summary>
        /// <returns>User Id in Interger Format</returns>
        public int? GetUidFromToken()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("eid")?.Value;
            if (int.TryParse(userId, out int eid))
            {
                return eid;
            }
            return null;
        }
        #endregion
    }
}
