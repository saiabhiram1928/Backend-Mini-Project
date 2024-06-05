using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IRefundRepository : IRepository<int,Refund>
    {
        public Task<Refund> GetRefundByOrderId(int orderId);
    }
}
