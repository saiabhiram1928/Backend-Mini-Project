using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class PermanentRepository:CRUDRepository<int,Permanent> , IPermanentRepository
    {
        public PermanentRepository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Permanent>> GetAll()
        {
            return await _context.Permanents.ToListAsync();
        }

        public override async Task<Permanent> GetById(int key)
        {
            var item =await _context.Permanents.SingleOrDefaultAsync(x=>x.Id == key);
            return item;
        }
    }
}
