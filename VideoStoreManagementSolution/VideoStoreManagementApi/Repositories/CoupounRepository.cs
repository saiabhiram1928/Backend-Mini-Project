using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class CoupounRepository : CRUDRepository<int,Coupoun>, ICoupounRepostiory
    {
        public CoupounRepository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Coupoun>> GetAll()
        {
            return await _context.Coupons.ToListAsync();
        }

        public override async Task<Coupoun> GetById(int key)
        {
            var item = await _context.Coupons.SingleOrDefaultAsync(c =>c.Id == key);
            return item;
        }

        public async Task<Coupoun> GetCoupounByCode(string code)
        {
           var item =await _context.Coupons.SingleOrDefaultAsync(c => c.CouponCode == code);
            return item;
        }
    }
}
