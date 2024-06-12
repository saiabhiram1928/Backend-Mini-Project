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
        public async Task MakePrimaryAddressFalse(int uid)
        {
            var isInMemory = _context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            //For Testing the method
            if (isInMemory)
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == uid && a.PrimaryAdress == true);
                if (address == null) return;
                address.PrimaryAdress = false;
                await _context.SaveChangesAsync();
                return;
            }
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var address = await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == uid && a.PrimaryAdress == true);
                    if (address == null) return;
                    address.PrimaryAdress = false;
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<bool> CheckAddressIsOfUser(int uid , int addressId)
        {
            var res = await _context.Addresses.AnyAsync(a => a.CustomerId == uid && a.Id == addressId);
            return res;
        }


    }
}
