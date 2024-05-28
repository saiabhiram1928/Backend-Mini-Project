using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class CartRepository : CRUDRepository<int, Cart>
    {
        public CartRepository(VideoStoreContext context) : base(context) { }
        public override async Task<IEnumerable<Cart>> GetAll()
        {
            var items = await _context.Carts.ToListAsync(); 
            return items;
        }

        public override async Task<Cart> GetById(int key)
        {
            var item = await _context.Carts.SingleOrDefaultAsync(Ca => Ca.Id == key);
            return item;
        }
    }
}
