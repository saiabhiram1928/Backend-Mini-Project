using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class OrderRepository : CRUDRepository<int, Order>
    {
        public OrderRepository(VideoStoreContext context) : base(context) { }   
        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public override async Task<Order> GetById(int key)
        {
            var item = await _context.Orders.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }
    }
}
