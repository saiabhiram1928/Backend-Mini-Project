using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface ICoupounRepostiory : IRepository<int,Coupoun>
    {
        public Task<Coupoun> GetCoupounByCode(string code);
    }
}
