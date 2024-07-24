using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class OrderRepository : CRUDRepository<string, Order> ,IOrderRepository
    {
        public OrderRepository(VideoStoreContext context) : base(context) 
        {

        }   
        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.Include(o => o.Rental).Include(o => o.Permanent).Include(o => o.DeliveryAddress).Include(o => o.Customer).Include(o => o.Payments).Include(o => o.Refund).Include(o => o.OrderItems).
                ToListAsync();
        }

        public override async Task<Order> GetById(int key)
        {
            var item = await _context.Orders.Include(o=> o.OrderItems).Include(o => o.DeliveryAddress).Include(o=>o.Payments).                SingleOrDefaultAsync(x => x.Id == key);
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
