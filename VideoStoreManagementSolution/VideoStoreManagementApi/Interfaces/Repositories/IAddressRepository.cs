using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IAddressRepository :IRepository<int, Address>
    {
        public Task<IEnumerable<Address>> GetAllAdressOfUser( int id);
        public Task MakePrimaryAddressFalse(int uid);
        public Task<bool> CheckAddressIsOfUser(int uid, int addressId);


    }
}
