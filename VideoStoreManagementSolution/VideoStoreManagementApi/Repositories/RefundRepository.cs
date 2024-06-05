using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class RefundRepository : CRUDRepository<int , Refund>, IRefundRepository
    {
        public RefundRepository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Refund>> GetAll()
        {
            return await _context.Refunds.ToListAsync();
        }

        public override async Task<Refund> GetById(int key)
        {
            var item = await _context.Refunds.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }
        public async Task<Refund> GetRefundByOrderId(int orderId)
        {
            var item = await _context.Refunds.SingleOrDefaultAsync(Order => Order.OrderId == orderId);
            return item;
        }

        
    }
}
