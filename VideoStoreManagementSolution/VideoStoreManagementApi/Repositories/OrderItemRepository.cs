using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class OrderItemRepository : CRUDRepository<int, OrderItem> , IOrderItemRepository
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
        public async Task<IEnumerable<OrderItem>> GetOrderItemsbyOrderId(int orderId)
        {
            var items = await _context.OrderItems.Include(oi => oi.Video)
                .Where(oi => oi.OrderId == orderId).ToListAsync();
            return items;
        }

    }
}
