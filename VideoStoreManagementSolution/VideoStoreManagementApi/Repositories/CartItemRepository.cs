using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class CartItemRepository : CRUDRepository<int, CartItem>
    {
        public CartItemRepository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<CartItem>> GetAll()
        {
           return await _context.CartItems.ToListAsync();
            
        }

        public override async Task<CartItem> GetById(int key)
        {
            var item = await _context.CartItems.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }
        

    }
}
