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

        public TokenService(IConfiguration configuration , IHttpContextAccessor httpContextAccessor)
        {
            _secretKey = configuration.GetSection("TokenKey").GetSection("JWT").Value.ToString();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            _httpContextAccessor = httpContextAccessor;
        }
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
        public int? GetUidFromToken()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("eid")?.Value;
            if (int.TryParse(userId, out int eid))
            {
                return eid;
            }
            return null;
        }
    }
}
