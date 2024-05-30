using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories { 


    public class CartItemRepository : CRUDRepository<int, CartItem>, ICartItemsRepository
    {
        public CartItemRepository(VideoStoreContext context) : base(context) { }

        public Task<bool> CheckCartItemExist(int videoId, int cartId)
        {
            _context.ChangeTracker.Clear();
            return _context.CartItems.AnyAsync(ci => ci.VideoId == videoId && ci.CartId == cartId);
        }

        public override async Task<IEnumerable<CartItem>> GetAll()
        {
            _context.ChangeTracker.Clear();
            return await _context.CartItems.ToListAsync();

        }

        public override async Task<CartItem> GetById(int key)
        {
            var item = await _context.CartItems.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }

        public async Task<CartItem> GetCartItemWithVideoId(int videoId, int cartId)
        {
            _context.ChangeTracker.Clear();
            var item = await _context.CartItems.Include(ci =>ci.Video).
                SingleOrDefaultAsync(ci => ci.VideoId == videoId && ci.CartId == cartId);
            return item;
        }
        public async Task<IEnumerable<CartItem>> GetCartItemsWithCartId(int cartId)
        {
            _context.ChangeTracker.Clear();
            var cartItems = await _context.CartItems.Include(ci => ci.Video)
                .Where(ci => ci.CartId == cartId).OrderBy(ci => ci.VideoId)
                .ToListAsync();
            return cartItems;
        }
    }
}
