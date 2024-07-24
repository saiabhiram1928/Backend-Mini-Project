using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class CartRepository : CRUDRepository<int, Cart>, ICartRepository
    {
        public CartRepository(VideoStoreContext context) : base(context) { }

        public async Task<bool> CheckCartExist(int uid)
        {
            _context.ChangeTracker.Clear();
            return await _context.Carts.AnyAsync(c=> c.CustomerId == uid);
        }

        public override async Task<IEnumerable<Cart>> GetAll()
        {
            var items = await _context.Carts.ToListAsync(); 
            return items;
        }

        public override async Task<Cart> GetById(int key)
        {
            
            var item = await _context.Carts.Include(c => c.CartItems)
                .SingleOrDefaultAsync(Ca => Ca.Id == key);
            return item;
        }

        public async Task<Cart> GetByUserId(int uid)
        {
            var item= await _context.Carts.SingleOrDefaultAsync(c => c.CustomerId == uid);
            return item;
        }
        public async Task<int> CartItemsCount(int uid)
        {
            var count = await _context.Carts.Include(c=>c.CartItems).SingleOrDefaultAsync(c => c.CustomerId==uid);
            if (count == null) return -1;
            return count.CartItems.Count();
        }
    }
}
