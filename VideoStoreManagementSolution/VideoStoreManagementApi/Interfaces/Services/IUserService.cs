using System.Runtime;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserReturnDTO> ViewProfile();
        public Task<UserReturnDTO> EditProfile(UserProfileEditDTO userProfileEditDTO);
        public  Task<string> ChangePassword(string newPasswd, string oldPasswd);
        public Task<IList<AddressDTO>> ViewAddress();
        public Task<AddressDTO> AddAddress(AddressRegisterDTO addressRegisterDTO);
        public Task<AddressDTO> EditAddress(AddressDTO addressDTO);
        public Task<string> DeleteAddress(int id);



    }
}
