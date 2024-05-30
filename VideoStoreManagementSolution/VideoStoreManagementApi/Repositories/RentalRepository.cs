using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class RentalRepository:CRUDRepository<int, Rental> , IRentalRepository
    {
        public RentalRepository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Rental>> GetAll()
        {
            return await _context.Rentals.ToListAsync();
        }

        public override async Task<Rental> GetById(int key)
        {
            var item = await _context.Rentals.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }
    }
}
