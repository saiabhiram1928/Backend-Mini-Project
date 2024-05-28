using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class AddressRepsitory : CRUDRepository<int,Address> ,IAddressRepository
    {
        public AddressRepsitory(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAdressOfUser(int id)
        {
            var address = await _context.Addresses.Where(a => a.CustomerId == id).ToListAsync();
            return address;
        }

        public override async Task<Address> GetById(int key)
        {
            var item = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }

    }
}
