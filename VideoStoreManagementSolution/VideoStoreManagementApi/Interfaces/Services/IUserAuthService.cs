using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IUserAuthService
    {
        public Task<UserReturnDTO> Login(UserLoginDTO userDTO);
        public Task<UserReturnDTO> Register(UserRegisterDTO userRegisterDTO);

    }
}
