using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IOrderRepository :IRepository<int,Order>
    {
        public Task<IList<Order>> GetOrdersByUid(int uid);
    }
}
