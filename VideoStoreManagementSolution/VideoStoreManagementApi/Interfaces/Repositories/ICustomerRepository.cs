using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<int, Customer>
    {
        public Task<MembershipType> GetMembershipType(int id);
    }
}
