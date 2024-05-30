using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<int, User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<bool> CheckUserExist(string email);
        public Task<bool> CheckUserExist(int id);
    }
}
