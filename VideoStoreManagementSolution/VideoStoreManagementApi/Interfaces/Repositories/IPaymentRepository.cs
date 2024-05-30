using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IPaymentRepository : IRepository<int,Payment>
    {
        public Task<Payment> GetPaymentByOrderId(int orderId);
    }
}
