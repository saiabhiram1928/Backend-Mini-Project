using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class UserRepository : CRUDRepository<int, User> , IUserRepository
    {
        public UserRepository(VideoStoreContext context) : base(context) { }

        public async override Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
               
            }
            return null;
        }

        public override async Task<User> GetById(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Uid == key);
            return user;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<bool> CheckUserExist(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }  
        public async Task<bool> CheckUserExist(int id)
        {
            return await _context.Users.AnyAsync(u => u.Uid == id);
        }
    }
}
