using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface ICartRepository : IRepository<int, Cart>
    {
        public Task<bool> CheckCartExist(int uid);
        public Task<Cart> GetByUserId(int uid);
    }
}
