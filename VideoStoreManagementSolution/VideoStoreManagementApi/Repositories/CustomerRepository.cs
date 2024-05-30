using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Repositories
{
    public class CustomerRepository : CRUDRepository<int,Customer>,ICustomerRepository
    {
        public CustomerRepository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Customer>> GetAll()
        {
            var items = await _context.Customers.ToListAsync();
            return items;
        }

        public override async Task<Customer> GetById(int key)
        {
            var item = await _context.Customers.Include(c => c.User).
                SingleOrDefaultAsync(c => c.Uid == key);
            return item;
        }

        public async Task<MembershipType> GetMembershipType(int id)
        {
            var item = await _context.Customers.SingleOrDefaultAsync(c => c.Uid == id);
            return item.MembershipType;

        }
    }
}
