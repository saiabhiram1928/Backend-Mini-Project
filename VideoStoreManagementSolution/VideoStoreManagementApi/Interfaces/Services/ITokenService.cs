using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public int? GetUidFromToken();
    }
}
