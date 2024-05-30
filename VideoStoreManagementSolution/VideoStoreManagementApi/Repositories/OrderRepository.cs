using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class OrderRepository : CRUDRepository<string, Order> ,IOrderRepository
    {
        public OrderRepository(VideoStoreContext context) : base(context) { }   
        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public override async Task<Order> GetById(int key)
        {
            var item = await _context.Orders.Include(o=> o.OrderItems).Include(o => o.DeliveryAddress).
                SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }
        public async Task<IList<Order>> GetOrdersByUid(int uid)
        {
            var item = await _context.Orders.Include(o => o.OrderItems).Include(o => o.DeliveryAddress)
                .Include(o => o.Payments)
                .Where(o => o.CustomerId == uid).ToListAsync();
            return item;
            
        }
    }
}
