using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IOrderItemRepository:IRepository<int,OrderItem>
    {
        public Task<IEnumerable<OrderItem>> GetOrderItemsbyOrderId(int orderId);
    }
}
