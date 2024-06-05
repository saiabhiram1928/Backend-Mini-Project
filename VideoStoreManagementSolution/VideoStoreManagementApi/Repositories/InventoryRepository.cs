using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class InventoryRepository : CRUDRepository<int, Inventory>,IInventoryRepository
    {
        public InventoryRepository(VideoStoreContext context) : base(context) { }
        public override async Task<IEnumerable<Inventory>> GetAll()
        {
            return await _context.Inventories.ToListAsync();
        }

        public override async Task<Inventory> GetById(int key)
        {
            var item = await _context.Inventories.SingleOrDefaultAsync(x => x.VideoId == key);
            return item;
        }
        public async Task<int> GetQty(int id)
        {
            var item = await _context.Inventories.SingleOrDefaultAsync(i => i.VideoId == id);
            return item != null ? item.Stock : 0;
        }
        public async Task<bool> UpdateStock(int qty, int videoId)
        {
            var item = await _context.Inventories.SingleOrDefaultAsync(i => i.VideoId == videoId);
            if (item == null) return false;
            item.Stock -= qty;
            return true;
        }
    }
}
