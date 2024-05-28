using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class OrderItemRepository : CRUDRepository<int, OrderItem>
    {
        public OrderItemRepository(VideoStoreContext context) : base(context) { }
        public override async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public override async Task<OrderItem> GetById(int key)
        {
           var item = await _context.OrderItems.SingleOrDefaultAsync(x =>x.Id == key);
            return item;
        }
        
    }
}
