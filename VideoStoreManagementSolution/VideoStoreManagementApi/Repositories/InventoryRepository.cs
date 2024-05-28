using Microsoft.EntityFrameworkCore;
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
    }
}
